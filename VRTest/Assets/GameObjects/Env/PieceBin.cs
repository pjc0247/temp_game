using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBin : MonoBehaviour {
    public static PieceBin instance;

    private GameObject spotlight;

    public PieceBin()
    {
        instance = this;
    }
    void Awake()
    {
        spotlight = transform.FindChild("Spotlight").gameObject;
        spotlight.SetActive(false);
    }

	void Start () {
		
	}

    public void EnableSpotlight(Piece piece)
    {
        spotlight.SetActive(true);
        spotlight.transform.position = piece.transform.position + new Vector3(0, 6, 0);
    }
    public void DisableSpotlight()
    {
        spotlight.SetActive(false);
    }
}
