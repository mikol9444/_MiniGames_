 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials;
public class _06_EnemySpawner : MonoBehaviour
{
   // Struct to store wave settings
     public float radius;
    [System.Serializable]
    public struct WaveSettings
    {
        public int numberOfEnemies;
        public float spawnInterval;
        public float waveInterval;
    }

    public Transform playerTransform;
    public float speed = 5f; // Adjust this speed as needed
    public GameObject enemyPrefab;
    public int currentWaveIndex = 0;
    public float timeBetweenWaves = 5f; // Adjust this time between waves as needed
    public WaveSettings[] waveSettings;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        // Iterate through each wave
        for (int waveIndex = 0; waveIndex < waveSettings.Length; waveIndex++)
        {
            WaveSettings currentWave = waveSettings[waveIndex];

            // Spawn enemies for the current wave
        for (int i = 0; i < currentWave.numberOfEnemies; i++)
        {
            // Calculate the position around the circle using polar coordinates
            float angle = i * 360f / currentWave.numberOfEnemies;
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float z = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

            Vector3 spawnPosition = playerTransform.position + new Vector3(x, 0f, z);

            // Instantiate the enemy at the calculated position
            GameObject enemy = ObjectPooler.Instance.GetObjectFromPool("Enemy");
            enemy.transform.position = spawnPosition;
            yield return new WaitForSeconds(currentWave.spawnInterval);
            enemy.SetActive(true);
        }

            // Wait for the specified interval before the next wave
            yield return new WaitForSeconds(currentWave.waveInterval);
        }
    }

}
