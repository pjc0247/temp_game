using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : MonoBehaviour
{
    public Vector3 target;
    public GameObject impulsePrefab;

    public float damage = 5;

    private SphereCollider bulletCollisder;
    private Vector3 initialPosition;
    private Vector3 speed;

    void Awake()
    {
        bulletCollisder = GetComponent<SphereCollider>();
    }
    void Start()
    {
        initialPosition = transform.position;
        speed = (target - initialPosition) / 10;

        Destroy(gameObject, 3.5f);
    }
    void Update()
    {
        transform.position += speed;

        var hit = false;
        // APPLY DAMAGE
        var mobs = new List<MobBase>(MobSpawner.instance.mobs);
        foreach (var mob in mobs)
        {
            var mobBounds = mob.GetComponent<Collider>().bounds;

            if (mobBounds.Intersects(bulletCollisder.bounds))
            {
                mob.impulse = (target - initialPosition).normalized;
                mob.playDeathParticle = false;
                mob.Damage(damage, DamageType.Bullet);

                hit = true;
            }
        }

        var dist = Vector3.Distance(target, transform.position);
        if (hit || dist <= 0.5f)
        {
            var particle = Instantiate(impulsePrefab);
            particle.transform.position = transform.position;

            Destroy(particle, 2.0f);
            Destroy(gameObject);
        }
    }
}
