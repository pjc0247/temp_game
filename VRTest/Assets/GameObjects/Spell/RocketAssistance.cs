using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NewtonVR;

public class RocketAssistance : SpellBase {
    private GameObject rocketPrefab;
    private GameObject impulsePrefab;

    private GameObject switchObject;

    protected override void Awake()
    {
        base.Awake();

        rocketPrefab = Resources.Load<GameObject>("Spell/RocketAssistance/Rocket");
        impulsePrefab = Resources.Load<GameObject>("Spell/RocketAssistance/Impulse");

        switchObject = transform.FindChild("Container").FindChild("Switch").gameObject;
    }

    public override void Cast()
    {
        StartCoroutine(ButtonAnimationFunc());
        StartCoroutine(CastFunc());
    }

    IEnumerator ButtonAnimationFunc()
    {
        for (int i = 0; i < 20; i++)
        {
            switchObject.transform.localPosition = new Vector3(0, 0.13f - i * 0.0065f, 0);
            yield return new WaitForEndOfFrame();
        }
        switchObject.transform.localPosition = new Vector3(0, 0.13f, 0);
    }
    IEnumerator CastFunc()
    {
        for (int i = 0; i < 360; i += 30)
        {
            var rocket = Instantiate(rocketPrefab);
            var parabolicComp = rocket.AddComponent<ParabolicBullet>();

            var x = Mathf.Sin(3.14f / 180 * i) * 10;
            var y = Mathf.Cos(3.14f / 180 * i) * 10;
            rocket.transform.position = new Vector3(x, -5, y);

            var targetX = Random.Range(-3.5f, 3.5f);
            var targetY = Random.Range(-3.5f, 3.5f);
            parabolicComp.target = new Vector3(targetX, 1, targetY);
            parabolicComp.time = 1.5f + Random.Range(0, 1.5f);
            parabolicComp.height = new Vector3(0, 15, 0);
            parabolicComp.damage = 100;
            parabolicComp.impulsePrefab = impulsePrefab;

            yield return new WaitForSeconds(0.2f);
        }
    }
}
