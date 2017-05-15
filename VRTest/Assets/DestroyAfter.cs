using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour {
    public float time = 1.0f;

	void Start () {
        Destroy(gameObject, time);
	}
}
