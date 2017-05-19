using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class StageSelector : MonoBehaviour {
    public static StageSelector instance;

    private GameObject pieceBinPrefab;
    private GameObject gameBoardPrefab;

    private GameObject[] previews;

    public StageSelector()
    {
        instance = this;
    }
    void Awake()
    {
        pieceBinPrefab = Resources.Load<GameObject>("Env/PieceBin");
        gameBoardPrefab = Resources.Load<GameObject>("Env/GameBoard");
    }
    void Start()
    {
        CreateBoards();
        StartCoroutine(MovePlayerFunc(new Vector3(0, -15.74f, -8.98f)));
    }

    void CreateBoards()
    {
        previews = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            var board = Instantiate(gameBoardPrefab);
            var gameBoardComp = board.GetComponent<GameBoard>();
            board.transform.position = new Vector3(i * 5.5f - 5.5f, 0, 0);
            board.AddComponent<BoardPreview>();

            if (i == 0)
                gameBoardComp.mapname = "map1";

            previews[i] = board;
        }
    }

    IEnumerator CreatePieceBinFunc()
    {
        var pieceBin = Instantiate(pieceBinPrefab);
        pieceBin.transform.position = new Vector3(10, 0, 0);
        
        for (int i = 0; i < 25; i++)
        {
            pieceBin.transform.localScale = new Vector3(
                i * 0.04f, i * 0.04f, i * 0.04f);

            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator MovePlayerFunc(Vector3 position)
    {
        var player = NVRPlayer.Instance;
        var original = player.transform.position;
        var target = position;

        for (int i = 0; i < 35; i++)
        {
            player.transform.position =
                player.transform.position + (target - player.transform.position) * 0.05f;

            yield return new WaitForEndOfFrame();
        }
    }

    public void SelectStage(GameObject board)
    {
        foreach (var preview in previews)
        {
            if (preview != board)
                preview.GetComponent<BoardPreview>().Fadeout();
        }

        SE.Play(Resources.Load<AudioClip>("SE/Item3"));

        StartCoroutine(CreatePieceBinFunc());
        StartCoroutine(MovePlayerFunc(new Vector3(5.38f, -13.74f, -8.98f)));
        board.GetComponent<GameBoard>().StartGame();
    }
}
