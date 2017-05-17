using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;

public class MobSpawner : MonoBehaviour {
    public static MobSpawner instance { get; set; }

    public List<MobBase> mobs = new List<MobBase>();
    public bool isSpawning { get; private set; }

    private LevelDesignData data;

    private GameObject spawnParticlePrefab;
    private GameObject mobPrefab;
    private int spawnCount = 10;
    private int spawnAmount = 3;
    private float spawnInterval = 2;
    private int level = 0;

    public MobSpawner()
    {
        instance = this;
    }

    void Awake()
    {
        LoadLevelDesignData();

        spawnParticlePrefab = Resources.Load<GameObject>("Effect/SpawnParticle");
    }
    void Start()
    {
        StartLevel(0);
    }

    void LoadLevelDesignData()
    {
        var json = Resources.Load<TextAsset>("Data/LevelDesign").text;

        data = JsonConvert.DeserializeObject<LevelDesignData>(json);

#if DEBUG
        Debug.Log(json);
        Debug.Log("Loaded " + data.levels.Count + " level(s)");
#endif
    }

    public void StartLevel(int level)
    {
        if (data.levels.Count <= level) return;
        
        mobPrefab = Resources.Load<GameObject>("Mob/Mob" + (level + 1).ToString());
        spawnAmount = data.levels[level].spawnAmount;
        spawnCount = data.levels[level].spawnCount;
        spawnInterval = data.levels[level].spawnInterval;

        StartCoroutine(SpawnFunc());

#if DEBUG
        Debug.Log(string.Format(
            "SpawnAmount : {0} \r\n" +
            "SpawnCount  : {1} \r\n" +
            "SpawnInterval : {2}",
            spawnAmount, spawnCount, spawnInterval));
#endif
    }

    void SpawnMob(float x, float y, bool randomizedOffset)
    {
        var mob = Instantiate(mobPrefab);
        var mobBaseComp = mob.GetComponent<MobBase>();
        mobBaseComp.SetPosition2D(x, y);
        mob.transform.SetParent(GameBoard.instance.transform);
        mob.transform.localPosition = new Vector3(0, 0.1f, 0);

        if (randomizedOffset)
        {
            mobBaseComp.offsetX = Random.Range(-0.4f, 0.4f);
            mobBaseComp.offsetY = Random.Range(-0.4f, 0.4f);
        }

        var particle = Instantiate(spawnParticlePrefab);
        particle.transform.SetParent(mob.transform);
        particle.transform.position = mob.transform.position;
        particle.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

        mobs.Add(mobBaseComp);
    }

    IEnumerator SpawnFunc()
    {
        var boardMeta = GameBoard.instance.meta;

        isSpawning = true;
        for (int i=0;i<spawnCount;i++)
        {
            yield return new WaitForSeconds(spawnInterval);

            for (int j = 0;j < spawnAmount; j++) {
                float x = boardMeta.startX;
                float y = boardMeta.startY;
                SpawnMob(x, y, spawnAmount == 1 ? false : true);
            }
        }
        isSpawning = false;

        StartCoroutine(ToNextLevelFunc());
    }
    IEnumerator ToNextLevelFunc()
    {
        yield return new WaitForSeconds(1);

        StartLevel(++level);
    }
}
