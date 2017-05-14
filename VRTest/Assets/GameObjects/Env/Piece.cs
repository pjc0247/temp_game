using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class Piece : MonoBehaviour {
    public GameObject towerPrefab;
    public Material thumbnailMat;

	void Start () {
		
	}

    public void OnHover()
    {
    }
    public void OnGrab()
    {
        var overlayPrefab = Resources.Load<GameObject>("Env/DeploymentOverlay");
        var overlay = Instantiate(overlayPrefab);
        var overlayComp = overlay.GetComponent<DeploymentOverlay>();

        overlay.transform.position = NVRPlayer.Instance.RightHand.transform.position;
        overlayComp.towerPrefab = towerPrefab;
        overlayComp.material = thumbnailMat;

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
