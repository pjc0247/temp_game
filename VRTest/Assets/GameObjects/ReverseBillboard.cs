using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(3000)]
public class ReverseBillboard : MonoBehaviour {
    private Quaternion initialRotation;

	void Awake () {
        initialRotation = transform.rotation;
	}
	void Update () {
        transform.rotation = initialRotation;
    }
}
