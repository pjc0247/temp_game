using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : MonoBehaviour
{
    public Vector3 target;
    public GameObject impulsePrefab;

    public float damage = 10;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;

        Destroy(gameObject, 3.5f);
    }
    void Update()
    {
        var targetPoint = target + new Vector3(0, 0.3f, 0);

        transform.position =
            transform.position + (targetPoint - transform.position) * 0.05f;

        var dist = Vector3.Distance(targetPoint, transform.position);
        if (dist <= 0.5f)
        {
            var particle = Instantiate(impulsePrefab);
            var impulseCollider = particle.GetComponent<SphereCollider>();
            particle.transform.position = transform.position;
            Destroy(particle, 2.0f);
            Destroy(gameObject);

            // APPLY DAMAGE
            var mobs = new List<MobBase>(MobSpawner.instance.mobs);
            foreach (var mob in mobs)
            {
                var mobBounds = mob.GetComponent<Collider>().bounds;

                if (mobBounds.Intersects(impulseCollider.bounds))
                    mob.Damage(damage, DamageType.Explosion);
            }
        }
    }
}
