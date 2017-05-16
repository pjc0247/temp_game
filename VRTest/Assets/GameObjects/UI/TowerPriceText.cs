using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPriceText : MonoBehaviour {
    public int price = 100;

    private TextMesh text;

    void Awake()
    {
        text = GetComponent<TextMesh>();
        text.text = price.ToString();
    }

	void Update () {          
        if (Wallet.gold >= price)
            text.color = Color.white;
        else
            text.color = Color.red;
	}
}
