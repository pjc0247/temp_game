using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class Piece : MonoBehaviour {
    public GameObject towerPrefab;
    public Material thumbnailMat;

    private GameObject infoObject;

    void Awake()
    {
        infoObject = transform.FindChild("Info").gameObject;
        infoObject.SetActive(false);
    }
	void Start () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == null) return;
        if (other.transform.parent.gameObject != NVRPlayer.Instance.RightHand.gameObject)
            return;

        infoObject.SetActive(true);
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform.parent == null) return;
        if (other.transform.parent.gameObject != NVRPlayer.Instance.RightHand.gameObject)
            return;

        infoObject.SetActive(false);
    }

    public void OnGrab()
    {
        var overlayPrefab = Resources.Load<GameObject>("Env/DeploymentOverlay");
        var overlay = Instantiate(overlayPrefab);
        var overlayComp = overlay.GetComponent<DeploymentOverlay>();

        overlay.transform.position = NVRPlayer.Instance.RightHand.transform.position;
        overlayComp.towerPrefab = towerPrefab;

        var interactableComp = overlay.GetComponent<NVRInteractableItem>();

        StartCoroutine(ProcessGrab(interactableComp));
    }

    IEnumerator ProcessGrab(NVRInteractableItem interactableComp)
    {
        yield return new WaitForEndOfFrame();

        NVRPlayer.Instance.RightHand.EndInteraction(NVRPlayer.Instance.RightHand.CurrentlyInteracting);
        NVRPlayer.Instance.RightHand.BeginInteraction(interactableComp);
    }
}
