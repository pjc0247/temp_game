using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class Gun : SpellBase
{
    private GameObject bulletPrefab;
    private GameObject impulsePrefab;

    private LineRenderer line;

    protected override void Awake()
    {
        base.Awake();

        bulletPrefab = Resources.Load<GameObject>("Spell/Gun/Bullet");
        impulsePrefab = Resources.Load<GameObject>("Spell/Gun/Impulse");

        line = GetComponentInChildren<LineRenderer>();
    }
    protected override void Update()
    {
        base.Update();

        var rightHand = NVRPlayer.Instance.RightHand;
        var endpoint = rightHand.transform.position + rightHand.transform.forward * 15;

        RaycastHit hit;
        if (Physics.Raycast(rightHand.transform.position, rightHand.transform.forward, out hit,
            100, LMask.GameBoard, QueryTriggerInteraction.Collide))
        {
            endpoint = hit.point;
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);
        line.SetPositions(new Vector3[] {
            rightHand.transform.position, endpoint
        });
    }

    protected override void OnCooldown()
    {
        line.enabled = false;
    }
    protected override void OnRestored()
    {
        line.enabled = true;
    }
    public override void Cast()
    {
        var rightHand = NVRPlayer.Instance.RightHand;
        var endpoint = rightHand.transform.position + rightHand.transform.forward * 15;

        var rocket = Instantiate(bulletPrefab);
        var bulletComp = rocket.AddComponent<ExplosionBullet>();
        rocket.transform.position = rightHand.transform.position;
        bulletComp.target = endpoint;
        bulletComp.impulsePrefab = impulsePrefab;
        bulletComp.damage = 2.5f;

        rightHand.LongHapticPulse(0.1f);
        //rightHand.TriggerHapticPulse(2000);
    }
    public override void OnEndGrab()
    {
        base.OnEndGrab();

        line.enabled = false;
    }
}
