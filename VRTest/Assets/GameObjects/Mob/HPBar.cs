using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour {
    public float hp = 100;
    public float maxHp = 100;

    private GameObject bar;
    
    void Awake()
    {
        bar = transform.FindChild("bar").gameObject;
    }

    public void SetHP(float _hp)
    {
        hp = _hp;

        Debug.Log(hp / maxHp);
        bar.transform.localScale = new Vector3(hp / maxHp, 1, 1);
    }
}
