using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class Rocket : SpellBase {
    private GameObject rocketPrefab;
    private GameObject impulsePrefab;

    private LineRenderer line;

    void Awake()
    {
        rocketPrefab = Resources.Load<GameObject>("Spell/RocketAssistance/Rocket");
        impulsePrefab = Resources.Load<GameObject>("Spell/Rocket/Impulse");

        line = gameObject.GetComponent<LineRenderer>();
    }
    protected override void Update()
    {
        base.Update();

        var rightHand = NVRPlayer.Instance.RightHand;
        var endpoint = rightHand.transform.position + rightHand.transform.forward * 10;

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

    public override void Cast()
    {
        var rightHand = NVRPlayer.Instance.RightHand;

        var rocket = Instantiate(rocketPrefab);

        rocket.transform.position = rightHand.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(rightHand.transform.position, rightHand.transform.forward, out hit,
            100, LMask.GameBoard, QueryTriggerInteraction.Collide))
        {
            var bulletComp = rocket.AddComponent<ParabolicBullet>();
            bulletComp.target = hit.point;
            bulletComp.impulsePrefab = impulsePrefab;
            bulletComp.time = 0.8f;
            bulletComp.damage = 10;
            bulletComp.height = new Vector3(0, 3, 0);
        }
    }
}
