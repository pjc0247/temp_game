using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {
	void Start () {
		
	}
	void Update () {
        var z = transform.rotation.eulerAngles.z;

        transform.LookAt(Camera.main.transform);
        transform.Rotate(new Vector3(0, 180, 0));

        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, z);
        //var camQ = Camera.main.transform.rotation;
        //camQ.SetLookRotation(transform);
        //transform.rotation = camQ;
        //transform.rotation = Quaternion.Euler(camRot.x, camRot.y, transform.rotation.eulerAngles.z);
    }
}
