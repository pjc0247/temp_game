using UnityEngine;
using System.Collections;

public class GunsDestroyByTime : MonoBehaviour {

    public float lifetime;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
