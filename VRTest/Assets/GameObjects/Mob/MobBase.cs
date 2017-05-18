using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobMovement))]
public class MobBase : BoardObject {
    public MobMovement movement;
    public GameObject deathParticle;

    public float hp = 10;
    public int gold = 10;

    public Vector3 impulse;

    private GameObject coinParticle;
    private HPBar hpBar;
    private Coroutine frostCoro;

    void Awake()
    {
        movement = GetComponent<MobMovement>();

        coinParticle = Resources.Load<GameObject>("Effect/CoinParticle");

        var hpBarPrefab = Resources.Load<GameObject>("Mob/HPBar");
        var hpBarObj = Instantiate(hpBarPrefab);
        hpBarObj.transform.SetParent(transform);
        hpBarObj.transform.localPosition = new Vector3(0, 1.2f, 0);
        hpBar = hpBarObj.GetComponent<HPBar>();
        hpBar.maxHp = hp;
        hpBar.SetHP(hp);

        gameObject.SetColor(Color.white);
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
        yield return new WaitForSeconds(1.8f);

        var renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var renderer in renderers)
            renderer.material.color = Color.white;
        var renderers2 = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var renderer in renderers2)
            renderer.material.color = Color.white;

        movement.SetMovementScale(1.0f);
    }

    public void Damage(float damage, DamageType type)
    {
        hp -= damage;
        hpBar.SetHP(hp);

        if (hp <= 0)
        {
            OnDeath();

            Wallet.gold += gold;

            if (type == DamageType.Explosion)
            {
                OnDeathByExplosion();
                RemoveObject(3);
            }
            else if (type == DamageType.Bullet)
            {
                OnDeathByBullet();
                RemoveObject(3);
            }
            else RemoveObject(0);
        }
    }
    protected virtual void OnDeath()
    {
        var particle = Instantiate(deathParticle);
        particle.transform.position = transform.position;

        particle = Instantiate(coinParticle);
        particle.transform.position = transform.position;
    }

    protected void RemoveObject(float delay)
    {
        Destroy(hpBar.gameObject);
        DisableMovement();
        Destroy(gameObject, delay);
        MobSpawner.instance.mobs.Remove(this);
    }
    protected void DisableMovement()
    {
        movement.enabled = false;
        movement.StopAllCoroutines();

        var waddle = gameObject.GetComponent<Waddle>();
        if (waddle != null)
            Destroy(waddle);
    }

    protected virtual void OnDeathByBullet()
    {
    }
    protected virtual void OnDeathByExplosion()
    {
        var rigidbody = gameObject.AddComponent<Rigidbody>();
        var angle = Random.Range(0, 359);
        var xVel = Mathf.Sin(3.14f / 180 * angle) * 1.0f;
        var yVel = Mathf.Cos(3.14f / 180 * angle) * 1.0f;
        rigidbody.AddForce(
            new Vector3(xVel, Random.Range(0.5f, 1.5f), yVel) * 5, ForceMode.Impulse);
        rigidbody.angularVelocity = new Vector3(xVel, yVel, xVel) * 5;
    }
}
