using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderTower : TowerBase
{
    public GameObject thunderParticle;

    Coroutine idleCoro;

    void Awake()
    {
        thunderParticle.SetActive(false);
    }

    protected override void OnAttack()
    {
        var target = GetTarget();
        if (target == null) return;

        var targetPoint = target.transform.position + new Vector3(0, 0.5f, 0);

        thunderParticle.transform.LookAt(targetPoint);

        thunderParticle.SetActive(true);

        if (idleCoro != null)
            StopCoroutine(idleCoro);
        idleCoro = StartCoroutine(IdleFunc());
    }
    IEnumerator IdleFunc()
    {
        yield return new WaitForSeconds(1.0f);

        thunderParticle.SetActive(false);
    }
}
