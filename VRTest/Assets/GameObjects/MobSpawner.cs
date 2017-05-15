using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour {
    public static MobSpawner instance { get; set; }

    public List<MobBase> mobs = new List<MobBase>();

    private GameObject mobPrefab;

    public MobSpawner()
    {
        instance = this;
    }

    void Start()
    {
        mobPrefab = Resources.Load<GameObject>("Mob/Mob3");

        StartCoroutine(SpawnFunc());
    }

    IEnumerator SpawnFunc()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);

            var mob = Instantiate(mobPrefab);
            var mobBaseComp = mob.GetComponent<MobBase>();
            mob.transform.SetParent(GameBoard.instance.transform);
            mob.transform.localPosition = new Vector3(0, 0.1f, 0);

            mobs.Add(mobBaseComp);
        }
    }
}
