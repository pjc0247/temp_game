using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {
    public bool x = true;
    public bool y = true;

	void Update () {
        var z = transform.rotation.eulerAngles.z;

        //        transform.LookAt(Camera.main.transform);
        //      transform.Rotate(new Vector3(0, 180, 0));

        var camRot = Camera.main.transform.eulerAngles;
        var original = transform.root.eulerAngles;
        transform.rotation = Quaternion.Euler(
            x ? camRot.x : original.x,
            y ? camRot.y : original.y,
            z);

        //var camQ = Camera.main.transform.rotation;
        //camQ.SetLookRotation(transform);
        //transform.rotation = camQ;
        //transform.rotation = Quaternion.Euler(camRot.x, camRot.y, transform.rotation.eulerAngles.z);
    }
}
