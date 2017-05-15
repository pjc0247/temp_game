using UnityEngine;
using System.Collections;

public class MineMover : MonoBehaviour {

    public float speed;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
    }
}
