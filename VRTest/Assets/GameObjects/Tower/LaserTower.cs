using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : TowerBase {
    public Transform firePoint;

    private GameObject startParticle;
    private GameObject endParticle;
    private GameObject beamParticle;

    void Awake()
    {
        startParticle = Resources.Load<GameObject>("Effect/LaserTower/RedLaserStart");
        endParticle = Resources.Load<GameObject>("Effect/LaserTower/RedLaserEnd");
        beamParticle = Resources.Load<GameObject>("Effect/LaserTower/RedLaserBeam");
    }

    protected override void OnAttack()
    {
        var target = GetTarget();
        if (target == null) return;

        var targetPoint = target.transform.position + new Vector3(0, 0.5f, 0);

        GetComponentInChildren<TurretHeadRotator>().SetHeadTo(targetPoint);

        var particle = Instantiate(endParticle);
        particle.transform.position = targetPoint;
        particle.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Destroy(particle, 0.15f);

        particle = Instantiate(startParticle);
        particle.transform.position = firePoint.position;
        particle.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Destroy(particle, 0.15f);

        particle = Instantiate(beamParticle);
        var line = particle.GetComponent<LineRenderer>();
        line.SetPositions(new Vector3[] {
            targetPoint, firePoint.position
        });
        line.startWidth = 0.7f; line.endWidth = 0.7f;
        Destroy(particle, 0.15f);

        target.Damage(attackDamage);
    }
}
