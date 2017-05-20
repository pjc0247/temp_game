using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerView : MonoBehaviour {
	void Start () {
        var r = GetComponent<Image>();
        var material = new Material(r.material);

        r.material = material;
        material.mainTexture = Resources.Load<RenderTexture>("UI/TowerView");
	}
}
