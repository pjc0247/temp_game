using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTower : TowerBase {
    public Collider flame1, flame2;

    protected override void OnAttack()
    {
        var targets = GetTargets();

        foreach (var target in targets)
        {
            var targetBounds = target.GetComponent<BoxCollider>().bounds;
            if (flame1.bounds.Intersects(targetBounds))
                target.Damage(attackDamage);
            if (flame2.bounds.Intersects(targetBounds))
                target.Damage(attackDamage);
        }
    }
}
