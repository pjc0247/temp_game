using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMove : MonoBehaviour {
    public bool loop = true;

	void Start () {
        var r = GetComponent<Renderer>();
        var texture = (MovieTexture)r.material.mainTexture;

        texture.Play();
        texture.loop = loop;
	}
}
