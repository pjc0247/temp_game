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

    protected override void OnAttack()
    {
        var target = GetTarget();
        if (target == null) return;

        var bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<Bullet>().target = target.gameObject;
        bullet.transform.position = firePoint.position;

        GetComponentInChildren<TurretHeadRotator>().SetHeadTo(
            target.transform.position + new Vector3(0, 0.4f, 0));
    }
}
