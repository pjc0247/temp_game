using UnityEngine;
using System.Collections;

public class TurretRotator : MonoBehaviour {
    public Transform target;
    public float speed;

    private bool idle = true;
    private Coroutine idleCoro;

    void Update()
    {
        if (idle == false) return;

        Vector3 targetDir = target.position - transform.position;
        float step = speed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    public void SetHeadTo(Vector3 vec)
    {
        idle = false;

        var angle = Vector3.Angle(transform.position, vec);
        //transform.rotation

        if (idleCoro != null)
            StopCoroutine(idleCoro);
        StartCoroutine(IdleFunc());
    }
    IEnumerator IdleFunc()
    {
        yield return new WaitForSeconds(1.5f);

        idle = true;
    }
}

