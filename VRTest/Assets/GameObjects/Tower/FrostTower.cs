using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostTower : TowerBase {
    private GameObject frostNovaPrefab;
    private GameObject frostEffectPrefab;

    void Awake()
    {
        frostNovaPrefab = Resources.Load<GameObject>("Effect/FrostTower/NovaBlue");
        frostEffectPrefab = Resources.Load<GameObject>("Effect/FrostTower/Frost");
    }

    protected override void OnAttack()
    {
        var targets = GetTargets();
        if (targets.Length == 0) return;

        foreach (var target in targets)
        {
            var frost = Instantiate(frostEffectPrefab);
            frost.transform.SetParent(target.transform);
            frost.transform.localPosition = Vector3.zero;
            frost.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

            target.Frost();
        }

        var particle = Instantiate(frostNovaPrefab);
        particle.transform.position = transform.position + new Vector3(0, 0.15f, 0);
        Destroy(particle, 1.0f);
    }
}
