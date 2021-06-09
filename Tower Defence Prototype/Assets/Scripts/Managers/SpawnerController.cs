using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> waveConfigs;
    [SerializeField] private bool isLooping;
    private List<Transform> spawnPoints = new List<Transform>();

    void Start()
    {
        GetSpawnPoints();
        StartCoroutine(BeginSpawning());
    }
    private void GetSpawnPoints()
    {
        foreach (Transform child in gameObject.transform)
        {
            //store all the Transforms of the spawnpoints in a list. gets the transform from the child objects of the spawner
            spawnPoints.Add(child);
        }
    }
    private IEnumerator BeginSpawning()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (isLooping);
    }
    private IEnumerator SpawnAllWaves()
    {
        //goes through all the wave configs in the list
        for(int i = 0; i < waveConfigs.Count; i++)
        {
            //store the current wave in a variable to pass to other methods
            var waveConfig = waveConfigs[i];

            //If there are simul waves in the config, start coroutines for spawning them, then move onto spawning the main wave
            StartCoroutine(SpawnSimultaneousWaves(waveConfig));

            if (waveConfig.IsClusteredSpawn)
            {
                //if the spawn type is clustered
                SpawnCluster(waveConfig);
            }
            else
            {
                //otherwise start coroutine to spawn enemies in a stream
                yield return StartCoroutine(SpawnEnemiesInWave(waveConfig));
            }

            //after current wave has finished spawning, wait for amount of time, spawning clustered waves in a loop needs a delay otherwise they all spawn add once essentially
            yield return new WaitForSeconds(waveConfig.DelayAfterWaveSpawned());
        }
    }
    private IEnumerator SpawnEnemiesInWave(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.NumberOfEnemies(); i++)
        {
            //spawn enemies in a stream, delay between spawns is taken from wave config
            Instantiate(waveConfig.EnemyPrefab(), spawnPoints[waveConfig.SpawnPoint()].transform.position, waveConfig.EnemyPrefab().transform.rotation);
            yield return new WaitForSeconds(waveConfig.TimeBetweenSpawns());
        }
    }
    private void SpawnCluster(WaveConfig waveConfig)
    {
        //spawns all enemies in the wave at once in a circular cluster shape
        for (int i = 0; i < waveConfig.NumberOfEnemies(); i++)
        {
            //Get a random normalized direction so that the cluster shape is circular instead of a square.
            var randomLocation = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;

            //Get random radius from 0 to the max radius in the waveconfig. this will make the enemies spawn anywhere within the max radius.
            var spawnLocation = spawnPoints[waveConfig.SpawnPoint()].transform.position + randomLocation * UnityEngine.Random.Range(0, waveConfig.ClusterRadius());

            Instantiate(waveConfig.EnemyPrefab(), spawnLocation, waveConfig.EnemyPrefab().transform.rotation);
        }
    }
    private IEnumerator SpawnSimultaneousWaves(WaveConfig waveConfig)
    {
        //if the config has simultaneous waves listed, spawn them, otherwise return 
        if (waveConfig.SimultaneousWaves() != null)
        {
            for (int i = 0; i < waveConfig.SimultaneousWaves().Count; i++)
            {
                //store current simul wave to pass to other methods
                var simultaneousWave = waveConfig.SimultaneousWaves()[i];

                if (simultaneousWave.IsClusteredSpawn)
                {
                    //if the simul wave is a clustered spawn
                    SpawnCluster(simultaneousWave);
                }
                else
                {
                    //if the simul wave is a constant spawn
                    StartCoroutine(SpawnEnemiesInWave(simultaneousWave));
                }
            }
        }
        else
        {
            yield return null;
        }
    }
}