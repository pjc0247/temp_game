using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class TowerBase : BoardObject {
    public float attackInterval = 1.0f;
    public int attackDamage = 10;
    public float range = 2.5f;

    public GameObject attackParticle;

    private GameObject rangeIndicator;

	protected override void Start () {
        base.Start();

        rangeIndicator = transform.FindChild("Range").gameObject;
        rangeIndicator.transform.localScale = new Vector3(1 + range*2, 1 + range*2, 1 + range*2);
        rangeIndicator.SetActive(false);

        StartCoroutine(AttackFunc());
	}

    IEnumerator AttackFunc()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);
            OnAttack();
        }
    }

    protected virtual void OnAttack()
    {
        var spanwer = MobSpawner.instance;
        if (spanwer.mobs.Count == 0) return;
         
        var position = new Vector2(transform.localPosition.x, transform.localPosition.z);

        foreach (var mob in spanwer.mobs)
        {
            var dist = Vector2.Distance(GetPosition2D(), mob.GetPosition2D());
            if (dist <= range)
            {
                var particle = Instantiate(attackParticle);
                particle.transform.SetParent(mob.transform);
                particle.transform.localPosition = Vector3.zero;

                mob.Damage(attackDamage);

                break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.gameObject != NVRPlayer.Instance.RightHand.gameObject)
            return;

        rangeIndicator.SetActive(true);
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.gameObject != NVRPlayer.Instance.RightHand.gameObject)
            return;

        rangeIndicator.SetActive(false);
    }
}
