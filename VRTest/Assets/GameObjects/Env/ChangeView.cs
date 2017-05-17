using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour {
    public Transform defaultTransform;
    public Transform leftTransform;
    public Transform rightTransform;
    public Transform rearTransform;

    private Coroutine moveCoro;

    void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
            MoveCameraTo(defaultTransform);
        if (Input.GetKeyDown(KeyCode.W))
            MoveCameraTo(leftTransform);
        if (Input.GetKeyDown(KeyCode.E))
            MoveCameraTo(rearTransform);
        if (Input.GetKeyDown(KeyCode.R))
            MoveCameraTo(rightTransform);
    }

    void MoveCameraTo(Transform target)
    {
        if (moveCoro != null)
            StopCoroutine(moveCoro);
        moveCoro = StartCoroutine(MoveCamera(target));
    }
    IEnumerator MoveCamera(Transform target)
    {
        for (int i = 0; i < 60; i++)
        {
            transform.position =
                transform.position + (target.position - transform.position) * 0.065f;
            transform.eulerAngles =
                transform.eulerAngles + (target.eulerAngles - transform.eulerAngles) * 0.065f;

            yield return new WaitForEndOfFrame();
        }
    }
}
