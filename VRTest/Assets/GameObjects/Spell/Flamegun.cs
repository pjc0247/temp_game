using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class Flamegun : ToggleSpellBase
{
    public Transform firepoint;

    private GameObject flamePrefab;

    private GameObject flame;
    private BoxCollider flameCollider;

    protected override void Awake()
    {
        base.Awake();

        flamePrefab = Resources.Load<GameObject>("Spell/Flamegun/Flame");
    }
    protected override void Update()
    {
        base.Update();

        var rightHand = NVRPlayer.Instance.RightHand;

        if (flame != null)
        {
            flame.transform.position = firepoint.position;
            flame.transform.rotation = firepoint.rotation;
        }
    }

    protected override void OnBeginCast()
    {
        flame = Instantiate(flamePrefab);
        flame.transform.position = firepoint.position;

        flameCollider = flame.GetComponent<BoxCollider>();
    }
    protected override void OnEndCast()
    {
        Destroy(flame);
        flame = null;
        flameCollider = null;
    }

    public override void Cast()
    {
        var rightHand = NVRPlayer.Instance.RightHand;

        if (flameCollider != null)
        {
            var mobs = new List<MobBase>(MobSpawner.instance.mobs);
            foreach (var mob in mobs)
            {
                var mobBounds = mob.GetComponent<Collider>().bounds;

                if (mobBounds.Intersects(flameCollider.bounds))
                {
                    mob.impulse = (mob.transform.position - flame.transform.position).normalized;
                    mob.playDeathParticle = false;
                    mob.Damage(1, DamageType.Bullet);
                }
            }
        }

        rightHand.LongHapticPulse(0.1f);
    }
}


