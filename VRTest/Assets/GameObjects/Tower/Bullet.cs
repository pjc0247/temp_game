using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public GameObject target;
    public GameObject impulsePrefab;

    public MobBase targetMob;
    public float damage = 0;

    private Vector3 initialPosition;

	void Start () {
        initialPosition = transform.position;

        Destroy(gameObject, 0.5f);
	}
	void Update () {
        var targetPoint = target.transform.position + new Vector3(0, 0.3f, 0);

        transform.position =
            transform.position + (targetPoint - transform.position) * 0.25f;

        var dist = Vector3.Distance(targetPoint, transform.position);
        if (dist <= 0.5f)
        {
            var particle = Instantiate(impulsePrefab);
            particle.transform.position = transform.position;
            Destroy(particle, 1.0f);
            Destroy(gameObject);

            if (targetMob != null)
            {
                targetMob.impulse = (targetPoint - initialPosition).normalized;
                targetMob.Damage(damage, DamageType.Bullet);
            }
        }
    }
}
