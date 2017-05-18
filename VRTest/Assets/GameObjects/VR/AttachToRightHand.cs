using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

[DefaultExecutionOrder(1000)]
public class AttachToRightHand : MonoBehaviour {
    void Start()
    {
        var rightHand = NVRPlayer.Instance.RightHand;
        transform.position = rightHand.transform.position;

        StartCoroutine(ProcessGrab(GetComponent<NVRInteractableItem>()));
    }

    IEnumerator ProcessGrab(NVRInteractableItem interactableComp)
    {
        yield return new WaitForEndOfFrame();

        var rightHand = NVRPlayer.Instance.RightHand;

        rightHand.EndInteraction(NVRPlayer.Instance.RightHand.CurrentlyInteracting);
        rightHand.BeginInteraction(interactableComp);
    }

    void Update2()
    {
        var rightHand = NVRPlayer.Instance.RightHand;
        transform.position = rightHand.transform.position;
        transform.rotation = rightHand.transform.rotation;
    }
}
