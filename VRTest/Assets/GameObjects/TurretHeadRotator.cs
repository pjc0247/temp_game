using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHeadRotator : MonoBehaviour {
    public float speed = 10;
    public bool x = false;
    public bool y = false;
    public bool z = true;

    private bool idle = true;
    private Coroutine idleCoro;

    void Update () {
        if (idle == false) return;

        transform.rotation *= Quaternion.Euler(
            x ? Time.deltaTime * speed : 0,
            y ? Time.deltaTime * speed : 0,
            z ? Time.deltaTime * speed : 0);
	}

    public void SetHeadTo(Vector3 target)
    {
        idle = false;

        //var angle = Vector3.Angle(transform.position, target.position);

        /*
        transform.rotation = Quaternion.Euler(
            x ? angle : transform.rotation.eulerAngles.x,
            y ? angle : transform.rotation.eulerAngles.y,
            z ? angle : transform.rotation.eulerAngles.z + 1);

        transform.Rotate(new Vector3(
            x ? angle : transform.rotation.eulerAngles.x,
            y ? angle : transform.rotation.eulerAngles.y,
            z ? angle : transform.rotation.eulerAngles.z));   
        Debug.Log(transform.rotation.eulerAngles.z);
        */

        transform.LookAt(target);
        transform.Rotate(new Vector3(-90, 0, 0));

        if (idleCoro != null)
            StopCoroutine(idleCoro);
        idleCoro = StartCoroutine(IdleFunc());
    }
    IEnumerator IdleFunc()
    {
        yield return new WaitForSeconds(1.5f);

        idle = true;
    }
}
