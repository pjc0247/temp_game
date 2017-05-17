using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballTower : TowerBase{
    public GameObject fireballPrefab;
    public GameObject launchParticlePrefab;

    public Transform firepoint;

    protected override bool OnAttack()
    {
        var target = GetTarget();
        if (target == null) return false;

        var particle = Instantiate(launchParticlePrefab);
        particle.transform.position = firepoint.transform.position;
        Destroy(particle, 1.0f);

        var fireball = Instantiate(fireballPrefab);
        var bulletComp = fireball.GetComponent<ParabolicBullet>();
        fireball.transform.position = firepoint.position;
        bulletComp.target = target.transform.position;
        bulletComp.damage = attackDamage;

        return true; 
    }
}
