using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config")]
public class WaveConfig : ScriptableObject
{
    [Header("Enemy Type")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int numberOfEnemies;

    [Header("Spawn Point")]
    [Range(0,11)][SerializeField] private int spawnPoint;

    [Header("Clustered Spawn")]
    [SerializeField] private bool isClusteredSpawn;
    [SerializeField] private float clusterRadius;

    [Header("Constant Spawn")]
    [SerializeField] private float timeBetweenSpawns;

    [Header("Delay After Wave Spawned")]
    [SerializeField] private float delayAfterWaveSpawned;

    [Header("Simultaneous Waves")]
    [SerializeField] private List<WaveConfig> simultaneousWaves;


    public GameObject EnemyPrefab() { return enemyPrefab; }
    public int NumberOfEnemies() { return numberOfEnemies; }
    public int SpawnPoint() { return spawnPoint; }
    public float TimeBetweenSpawns() { return timeBetweenSpawns; }
    public bool IsClusteredSpawn
    {
        get
        {
            return isClusteredSpawn;
        }
    }
    public float ClusterRadius() { return clusterRadius; }
    public float DelayAfterWaveSpawned() { return delayAfterWaveSpawned; }
    public List<WaveConfig> SimultaneousWaves() { return simultaneousWaves; }
}
