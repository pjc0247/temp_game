using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public int level;

    /// <summary>스폰 사이 간격</summary>
    public float spawnInterval;
    /// <summary>한번 스폰 시 동시 생성 갯수</summary>
    public int spawnAmount;
    /// <summary>스폰 횟수</summary>
    public int spawnCount;

    public LevelData()
    {
        spawnCount = 1;
        spawnInterval = 1.0f;
        spawnAmount = 1;
    }
}

public class LevelDesignData
{
    public List<LevelData> levels;

    public LevelDesignData()
    {
        levels = new List<LevelData>();
    }
}