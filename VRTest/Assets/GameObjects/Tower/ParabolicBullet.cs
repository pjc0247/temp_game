using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicBullet : MonoBehaviour {
    public Vector3 target;
    public GameObject impulsePrefab;

    public Vector3 height = new Vector3(0, 2.35f, 0);
    public float time = 1.5f;

    public float damage;

    void Awake()
    {
    }
    void Start()
    {
        StartCoroutine(MoveFunc());
    }

    public Vector3[] ParabolicMovement(Vector3 startingPos, Vector3 arrivingPos)
    {
        int framesNum = (int)(time * 20);
        Vector3[] frames = new Vector3[framesNum + 1];

        //PROJECTING ON Z AXIS
        Vector3 stP = new Vector3(0, startingPos.y, startingPos.z);
        Vector3 arP = new Vector3(0, arrivingPos.y, arrivingPos.z);

        Vector3 diff = new Vector3();

        diff = ((arP - stP) / 2) + height;
        Vector3 vertex = stP + diff;

        float x1 = startingPos.z;
        float y1 = startingPos.y;
        float x2 = arrivingPos.z;
        float y2 = arrivingPos.y;
        float x3 = vertex.z;
        float y3 = vertex.y;

        float denom = (x1 - x2) * (x1 - x3) * (x2 - x3);

        var z_dist = (arrivingPos.z - startingPos.z) / framesNum;
        var x_dist = (arrivingPos.x - startingPos.x) / framesNum;

        float A = (x3 * (y2 - y1) + x2 * (y1 - y3) + x1 * (y3 - y2)) / denom;
        float B = (float)(System.Math.Pow(x3, 2) * (y1 - y2) + System.Math.Pow(x2, 2) * (y3 - y1) + System.Math.Pow(x1, 2) * (y2 - y3)) / denom;
        float C = (x2 * x3 * (x2 - x3) * y1 + x3 * x1 * (x3 - x1) * y2 + x1 * x2 * (x1 - x2) * y3) / denom;

        float newX = startingPos.z;
        float newZ = startingPos.x;

        frames[0] = startingPos;
        for (int i = 1; i <= framesNum; i++)
        {
            newX += z_dist;
            newZ += x_dist;
            float yToBeFound = A * (newX * newX) + B * newX + C;
            frames[i] = new Vector3(newZ, yToBeFound, newX);
        }
        return frames;
    }
    IEnumerator MoveFunc()
    {
        var frames = ParabolicMovement(transform.position, target);

        for (int i = 0; i < frames.Length; i++)
        {
            transform.position = frames[i];
            yield return new WaitForSeconds(0.016f * 3); // 20fps
        }
        Destroy(gameObject);

        var impulse = Instantiate(impulsePrefab);
        var impulseCollider = impulse.GetComponent<SphereCollider>();
        impulse.transform.position = target;
        Destroy(impulse, 1.0f);

        // APPLY DAMAGE
        var mobs = new List<MobBase>(MobSpawner.instance.mobs);
        foreach (var mob in mobs)
        {
            var mobBounds = mob.GetComponent<Collider>().bounds;

            if (mobBounds.Intersects(impulseCollider.bounds))
                mob.Damage(damage, DamageType.Explosion);
        }
    }
}
