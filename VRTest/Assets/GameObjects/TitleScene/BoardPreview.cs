using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class BoardPreview : MonoBehaviour {
    private static BoardPreview hovered;

    private Coroutine scaleCoro;
    private GameObject selectionParticlePrefab;

    private bool hover = false;
    private bool selected = false;

    void Awake()
    {
        selectionParticlePrefab = Resources.Load<GameObject>("Effect/SelectionParticle");
    }
    void Start()
    {
        scaleCoro = StartCoroutine(ScaleFunc(0.35f));
    }
    void Update()
    {
        if (hover == false) return;
        if (selected) return;

        transform.Rotate(new Vector3(0, 0.1f, 0));

        var rightHand = NVRPlayer.Instance.RightHand;
        if (rightHand.UseButtonPressed)
            OnSelect();
    }

    void OnSelect()
    {
        var rightHand = NVRPlayer.Instance.RightHand;
        var particle = Instantiate(selectionParticlePrefab);
        particle.transform.position = rightHand.transform.position;

        if (scaleCoro != null)
            StopCoroutine(scaleCoro);
        StartCoroutine(ScaleFunc(1.0f));
        StartCoroutine(MoveFunc());

        transform.rotation = Quaternion.Euler(0, 0, 0);
        StageSelector.instance.SelectStage(gameObject);

        selected = true;
        hovered = null;
    }
    void OnTriggerEnter(Collider other)
    {
        if (selected) return;
        if (other.transform.parent == null) return;
        if (other.transform.parent.gameObject != NVRPlayer.Instance.RightHand.gameObject)
            return;

        if (hovered != null && hovered != this)
        {
            hovered.OnTriggerExit(other);
            hovered = this;
        }

        if (scaleCoro != null)
            StopCoroutine(scaleCoro);
        scaleCoro = StartCoroutine(ScaleFunc(0.6f));
        hover = true;

        SE.Play(Resources.Load<AudioClip>("SE/Cursor"));
    }
    void OnTriggerExit(Collider other)
    {
        if (selected) return;
        if (other.transform.parent == null) return;
        if (other.transform.parent.gameObject != NVRPlayer.Instance.RightHand.gameObject)
            return;

        if (scaleCoro != null)
            StopCoroutine(scaleCoro);
        scaleCoro = StartCoroutine(ScaleFunc(0.35f));
        hover = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);  

        if (hovered == this) hovered = null;
    }

    IEnumerator ScaleFunc(float scale)
    {
        var original = transform.localScale;
        var target = new Vector3(scale, scale, scale);

        for (int i = 0; i < 30; i++)
        {
            transform.localScale = transform.localScale + (target - transform.localScale) * 0.07f;

            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator MoveFunc()
    {
        var original = transform.localScale;
        var target = new Vector3(0,0,0);

        for (int i = 0; i < 30; i++)
        {
            transform.position = transform.position + (target - transform.position) * 0.07f;

            yield return new WaitForEndOfFrame();
        }

        Destroy(this);
    }
    public void Fadeout()
    {
        StartCoroutine(ScaleFunc(0));
        Destroy(gameObject, 0.5f);
    }
}
