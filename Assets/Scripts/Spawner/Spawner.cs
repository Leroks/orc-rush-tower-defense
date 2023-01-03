using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnModes
{
    Fixed,
    Random
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnModes spawnMode = SpawnModes.Fixed;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private GameObject testGO;

    [Header("Fixed Delay")]
    [SerializeField] private float delayBetweenSpawns;

    [Header("Random Delay")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;

    private float spawnTimer;
    private int enemiesSpawned;

    private ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = GetComponent<ObjectPooler>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = GetSpawnDelay();
            if (enemiesSpawned < enemyCount)
            {
                SpawnEnemy();
                enemiesSpawned++;
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = objectPooler.GetPooledObject();
        enemy.SetActive(true);
    }

    // Returns the delay between spawns based on the spawn mode
    private float GetSpawnDelay()
    {
        switch (spawnMode)
        {
            case SpawnModes.Fixed:
                return delayBetweenSpawns;
            case SpawnModes.Random:
                return GetRandomDelay();
            default:
                return 0;
        }
    }

    // Returns a random delay between the min and max values
    private float GetRandomDelay()
    {
        return Random.Range(minRandomDelay, maxRandomDelay);
    }
}
