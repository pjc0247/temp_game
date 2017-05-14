using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobMovement))]
public class MobBase : BoardObject {
    public MobMovement movement;
    public GameObject deathParticle;

    public int hp = 10;

    void Awake()
    {
        movement = GetComponent<MobMovement>();
    }

    public void Damage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            OnDeath();
            DestroyObject(gameObject);
            MobSpawner.instance.mobs.Remove(this);
        }
    }
    protected virtual void OnDeath()
    {
        var particle = Instantiate(deathParticle);
        particle.transform.position = transform.position;
    }
}
