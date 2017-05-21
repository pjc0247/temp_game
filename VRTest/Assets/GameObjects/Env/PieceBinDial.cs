using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBinDial : MonoBehaviour {
    private bool grab = false;

    private Coroutine rotateCoro;

	void Start () {
		
	}

	void Update () {
        if (grab == false) return;

        PieceBin.instance.transform.eulerAngles =
            new Vector3(0, 0, transform.eulerAngles.z);
	}

    public void OnBeginGrab()
    {
        grab = true;
    }
    public void OnEndGrab()
    {
        grab = false;

        if (rotateCoro != null)
            StopCoroutine(rotateCoro);

        var z = PieceBin.instance.transform.eulerAngles.z;

        if (z >= 0 && z < 90)
            rotateCoro = StartCoroutine(RotateFunc(0));
        else if (z >= 360-90 && z < 360)
            rotateCoro = StartCoroutine(RotateFunc(360));
        else if (z >= 90 && z < 180)
            rotateCoro = StartCoroutine(RotateFunc(180));
        else
            rotateCoro = StartCoroutine(RotateFunc(180));
    }

    IEnumerator RotateFunc(float z)
    {
        var t = PieceBin.instance.transform;

        for (int i = 0; i < 25; i++)
        {
            t.eulerAngles =
                new Vector3(0, 0, Mathf.Max(t.eulerAngles.z + (z - t.eulerAngles.z) * 0.25f, z));

            yield return new WaitForEndOfFrame();
        }
    }
}
