using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour {
    public static MobSpawner instance { get; set; }

    public List<MobBase> mobs = new List<MobBase>();
    public bool isSpawning { get; private set; }

    private GameObject spawnParticlePrefab;
    private GameObject mobPrefab;
    private int spawnCount = 10;
    private float spawnInterval = 2;

    public MobSpawner()
    {
        instance = this;
    }

    void Awake()
    {
        spawnParticlePrefab = Resources.Load<GameObject>("Effect/SpawnParticle");
    }
    void Start()
    {
        StartLevel(1);
    }

    public void StartLevel(int level)
    {
        mobPrefab = Resources.Load<GameObject>("Mob/Mob2");
        spawnCount = 15;
        spawnInterval = 1.1f;

        StartCoroutine(SpawnFunc());
    }
    IEnumerator SpawnFunc()
    {
        isSpawning = true;
        for (int i=0;i<spawnCount;i++)
        {
            yield return new WaitForSeconds(spawnInterval);

            var mob = Instantiate(mobPrefab);
            var mobBaseComp = mob.GetComponent<MobBase>();
            mobBaseComp.SetPosition2D(0, 0);
            mob.transform.SetParent(GameBoard.instance.transform);
            mob.transform.localPosition = new Vector3(0, 0.1f, 0);

            var particle = Instantiate(spawnParticlePrefab);
            particle.transform.SetParent(mob.transform);
            particle.transform.localPosition = new Vector3(0, 1, 0);
            particle.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            mobs.Add(mobBaseComp);
        }
        isSpawning = false;
    }
}
