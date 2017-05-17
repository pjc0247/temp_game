using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour {
    public static int gold = 200;

    private TextMesh goldText;

    void Awake()
    {
        goldText = transform.FindChild("GoldText")
            .GetComponent<TextMesh>();
    }
    void Update()
    {
        goldText.text = gold.ToString();
    }
}
