using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour {
    public static int gold = 200;

    public TextMesh goldText;

    void Awake()
    {
    }

    void Update()
    {
        goldText.text = gold.ToString();
    }
}
