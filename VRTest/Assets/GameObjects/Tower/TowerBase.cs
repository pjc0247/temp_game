using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class TowerBase : BoardObject {
    public bool prewview = false;

    public float attackInterval = 1.0f;
    public float attackDamage = 1.0f;
    public float range = 2.5f;

    public GameObject attackParticle;

    private GameObject rangeIndicator;

	protected override void Start () {
        base.Start();

        rangeIndicator = transform.FindChild("Range").gameObject;
        rangeIndicator.transform.localScale = new Vector3(1 + range*2, 1 + range*2, 1 + range*2);
        rangeIndicator.SetActive(false);

        if (prewview == false)
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

    protected MobBase GetTarget()
    {
        var spanwer = MobSpawner.instance;
        if (spanwer.mobs.Count == 0) return null;

        var position = new Vector2(transform.localPosition.x, transform.localPosition.z);

        foreach (var mob in spanwer.mobs)
        {
            var dist = Vector2.Distance(GetPosition2D(), mob.GetPosition2D());
            if (dist <= range)
                return mob;
        }
        return null;
    }
    protected MobBase[] GetTargets()
    {
        var result = new List<MobBase>();

        var spanwer = MobSpawner.instance;
        if (spanwer.mobs.Count == 0) return result.ToArray();

        var position = new Vector2(transform.localPosition.x, transform.localPosition.z);

        foreach (var mob in spanwer.mobs)
        {
            var dist = Vector2.Distance(GetPosition2D(), mob.GetPosition2D());
            if (dist <= range)
                result.Add(mob);
        }

        return result.ToArray();
    }

    protected virtual void OnAttack()
    {
        var target = GetTarget();
        if (target == null) return;

        var particle = Instantiate(attackParticle);
        particle.transform.SetParent(target.transform);
        particle.transform.localPosition = Vector3.zero;

        target.Damage(attackDamage);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == null) return;
        if (other.transform.parent.gameObject != NVRPlayer.Instance.RightHand.gameObject)
            return;

        rangeIndicator.SetActive(true);
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform.parent == null) return;
        if (other.transform.parent.gameObject != NVRPlayer.Instance.RightHand.gameObject)
            return;

        rangeIndicator.SetActive(false);
    }
}
