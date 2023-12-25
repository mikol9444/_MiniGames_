 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials;
using TMPro;
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
    public ExampleInputListener listener;
    public WaveSettings[] waveSettings;
    public TextMeshProUGUI textMesh;
    public int enemyCount;

    void Start()
    {
        listener = FindObjectOfType<ExampleInputListener>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // StartCoroutine(SpawnWaves());

    }
    private void Update() {
        if (listener.movementVector== Vector2.zero)
        {
            return;
        }
        else{
            GameObject enemy = ObjectPoolManager.SpawnObject(enemyPrefab,transform);
            Vector3 spawnPosition = playerTransform.position + new Vector3(-listener.movementVector.x, 0f,-listener.movementVector.y)*radius;
             enemy.transform.position = spawnPosition;
             enemyCount++;
             textMesh.text =enemyCount.ToString();
        }
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
            GameObject enemy = Instantiate(enemyPrefab);
            if(enemy){
            enemy.transform.position = spawnPosition;
            enemy.SetActive(true);
            }
            yield return new WaitForSeconds(currentWave.spawnInterval);
        }

            // Wait for the specified interval before the next wave
            yield return new WaitForSeconds(currentWave.waveInterval);
        }
    }

}
