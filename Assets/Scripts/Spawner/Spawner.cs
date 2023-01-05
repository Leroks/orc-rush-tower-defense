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
    [SerializeField] private float delayBetweenWaves = 1f;

    [Header("Fixed Delay")]
    [SerializeField] private float delayBetweenSpawns;

    [Header("Random Delay")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;

    private float spawnTimer;
    private int enemiesSpawned;
    private int enemiesRemaining;

    private ObjectPooler objectPooler;
    private Waypoint waypoint;

    private void Start()
    {
        objectPooler = GetComponent<ObjectPooler>();
        waypoint = GetComponent<Waypoint>();

        enemiesRemaining = enemyCount;
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
        GameObject enemyInstance = objectPooler.GetPooledObject();
        Enemy enemy = enemyInstance.GetComponent<Enemy>();
        enemy.Waypoint = waypoint;
        enemy.ResetEnemy();

        enemy.transform.localPosition = transform.position;
        enemyInstance.SetActive(true);
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

    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBetweenWaves);
        enemiesRemaining = enemyCount;
        spawnTimer = 0;
        enemiesSpawned = 0;
    }

    private void RecordEnemy()
    {
        enemiesRemaining--;
        if (enemiesRemaining <= 0)
        {
            // Wave Complete
            StartCoroutine(NextWave());
        }
    }

    private void OnEnable()
    {
        Enemy.OnEnemyReachedEnd += RecordEnemy;
        EnemyHealth.OnEnemyKilled += RecordEnemy;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyReachedEnd -= RecordEnemy;
        EnemyHealth.OnEnemyKilled -= RecordEnemy;
    }
}
