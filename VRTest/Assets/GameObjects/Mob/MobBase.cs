﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobMovement))]
public class MobBase : BoardObject {
    public MobMovement movement;
    public GameObject deathParticle;

    public float hp = 10;

    private HPBar hpBar;
    private Coroutine frostCoro;

    void Awake()
    {
        movement = GetComponent<MobMovement>();

        var hpBarPrefab = Resources.Load<GameObject>("Mob/HPBar");
        var hpBarObj = Instantiate(hpBarPrefab);
        hpBarObj.transform.SetParent(transform);
        hpBarObj.transform.localPosition = new Vector3(0, 16, 0);
        hpBar = hpBarObj.GetComponent<HPBar>();
        hpBar.maxHp = hp;
        hpBar.SetHP(hp);
    }

    public void Frost()
    {
        if (frostCoro != null)
            StopCoroutine(frostCoro);

        var renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var renderer in renderers)
            renderer.material.color = Color.blue;
        var renderers2 = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var renderer in renderers2)
            renderer.material.color = Color.blue;

        movement.SetMovementScale(0.5f);
        frostCoro = StartCoroutine(FrostFunc());
    }
    IEnumerator FrostFunc()
    {
        yield return new WaitForSeconds(1.0f);

        var renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var renderer in renderers)
            renderer.material.color = Color.white;
        var renderers2 = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var renderer in renderers2)
            renderer.material.color = Color.white;

        movement.SetMovementScale(1.0f);
    }

    public void Damage(float damage)
    {
        hp -= damage;
        hpBar.SetHP(hp);

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
