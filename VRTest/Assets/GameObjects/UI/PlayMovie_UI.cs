using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMovie_UI : MonoBehaviour {
    public bool loop = true;
    public MovieTexture movie;

    private Material material;

	void Start () {
        var image = GetComponent<Image>();

        material = image.material = new Material(image.material);
        material.mainTexture = movie;

        movie.Play();
        movie.loop = loop;
	}

    public void Stop()
    {
        movie.Stop();
    }
    public void PlayFromStart()
    {
        movie.Play();
    }
}
