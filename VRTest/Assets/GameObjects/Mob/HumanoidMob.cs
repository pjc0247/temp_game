using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidMob : MobBase {

    // 휴머노이드 타입은 총알에 죽으면 뒤로 밀림
    protected override void OnDeathByBullet()
    {
        var rigidbody = gameObject.AddComponent<Rigidbody>();
        var force = Random.Range(5, 8);
        rigidbody.AddForce(impulse * force, ForceMode.Impulse);
    }
}
