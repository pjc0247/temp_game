using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTower : TowerBase {
    public Transform firePoint;

    private GameObject bulletPrefab;

    void Awake()
    {
        bulletPrefab = Resources.Load<GameObject>("Tower/Bullets/RedBullet");
    }

    protected override bool OnAttack()
    {
        var target = GetTarget();
        if (target == null) return false;

        var bullet = Instantiate(bulletPrefab);
        var bulletComp = bullet.GetComponent<Bullet>();
        bulletComp.target = target.gameObject;
        bulletComp.targetMob = target;
        bulletComp.damage = attackDamage;
        bullet.transform.position = firePoint.position;

        GetComponentInChildren<TurretHeadRotator>().SetHeadTo(
            target.transform.position + new Vector3(0, 0.4f, 0));

        return true;
    }
}
