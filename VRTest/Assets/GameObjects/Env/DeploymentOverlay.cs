using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class DeploymentOverlay : MonoBehaviour {
    public GameObject towerPrefab;
    public int price;

    private NVRHand hand;
    private bool insideBoard;
    private bool grab;

    private GameObject deployParticlePrefab;
    private GameObject thumbnail;
    private GameObject rangeIndicator;
    private LineRenderer lineRenderer;
    private Tile currentTile;

    void Awake()
    {
        deployParticlePrefab = Resources.Load<GameObject>("Effect/DeployParticle");
    }
	void Start () {
        insideBoard = false;
        grab = true;

        thumbnail = Instantiate(towerPrefab);
        thumbnail.GetComponent<TowerBase>().preview = true;
        thumbnail.transform.SetParent(transform);
        thumbnail.transform.localPosition = Vector3.zero;
        thumbnail.SetOpacity(0.5f);

        var range = towerPrefab.GetComponent<TowerBase>().range;
        rangeIndicator = transform.FindChild("Range").gameObject;
        rangeIndicator.transform.localScale = new Vector3(1 + range * 2, 1 + range * 2, 1 + range * 2);

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startColor = Color.red;
        lineRenderer.startWidth = 0.3f;
        lineRenderer.endColor = Color.black;
        lineRenderer.endWidth = 0.01f;

        UIEffect.StartGrabHaptic();
    }
    void Update()
    {
        if (grab)
            thumbnail.transform.localPosition = Vector3.zero;

        if (insideBoard == false) return;

        currentTile = GameBoard.instance.GetTileFromPosition(transform.position);
        GameBoard.instance.HighlightTile(currentTile);
        rangeIndicator.GetComponent<MeshRenderer>().material.SetFloat("_Opacity", 0.3f);
        rangeIndicator.transform.position = currentTile.transform.position + new Vector3(0, 0.01f, 0);
        rangeIndicator.transform.rotation = Quaternion.Euler(90, 0, 0);

        lineRenderer.SetPositions(new Vector3[] {
            transform.position,
            currentTile.transform.position
        });
    }

    /// <summary>
    /// 타워를 보드에 실제로 배치한다
    /// </summary>
    void Deploy()
    {
        currentTile.occupied = true;

        var tower = GameBoard.instance.AddObject(towerPrefab);
        var boardXY = tower.GetComponent<BoardObject>();
        boardXY.SetPosition2D(currentTile.x, currentTile.y);

        var particle = Instantiate(deployParticlePrefab);
        particle.transform.position = tower.transform.position;

        Wallet.gold -= price;
    }

    public void OnGrab()
    {
        grab = true;

        hand = GetComponent<NVRInteractableItem>().AttachedHand;
        hand.SetVisibility(VisibilityLevel.Invisible);
    }
    public void OnEndGrab()
    {
        grab = false;

        if (insideBoard)
        {
            if (currentTile != null && currentTile.IsBuildable())
                Deploy();
            else
                SE.Play(Resources.Load<AudioClip>("SE/ErrorSE"));

            Destroy(gameObject);
        }
        else
            Destroy(gameObject, 6); // 던지기 효과를 위해 늦게 지움

        hand.SetVisibility(VisibilityLevel.Visible);
        insideBoard = false;
        GameBoard.instance.UnhighlightTile();
        lineRenderer.enabled = false;
        rangeIndicator.SetActive(false);

        UIEffect.StopGrabHaptic();
    }

    void OnTriggerEnter(Collider other)
    {
        if (grab == false) return;
        if (GameBoard.instance.gameObject != other.gameObject)
            return;

        insideBoard = true;
        lineRenderer.enabled = true;
        StartCoroutine(ScaleFunc(1.0f));

        rangeIndicator.SetActive(true);
    }
    void OnTriggerExit(Collider other)
    {
        if (GameBoard.instance.gameObject != other.gameObject)
            return;

        insideBoard = false;
        lineRenderer.enabled = false;
        GameBoard.instance.UnhighlightTile();
        StartCoroutine(ScaleFunc(3.0f));

        rangeIndicator.SetActive(false);
    }

    IEnumerator ScaleFunc(float scaleTo)
    {
        var initialScale = transform.localScale;
        var step = (transform.localScale - (new Vector3(1, 1, 1) * scaleTo)) / 20;

        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForEndOfFrame();

            transform.localScale = initialScale - (i * step);
        }
    }
}
