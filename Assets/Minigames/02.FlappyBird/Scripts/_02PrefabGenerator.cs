using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials;
public class _02PrefabGenerator : MonoBehaviour
{
    public float minSpawnRangeTopY, maxSpawnRangeTopY, minSpawnRangeBotY, maxSpawnRangeBotY, distance;
    public Vector3 upVector;
    public Vector3 downVector;
    public float returnTimer = 1.5f;
    public float spawnTimer = 1f;
    private Transform playerTransform;
   public float frequency = 1f; // Adjust this value to control the frequency of the wave
    public float amplitude = 1f;
    public float threshHold = 1.5f;
    private void OnValidate()
    {
        
    }
    private void Awake()
    {
        if (!playerTransform) playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    public void StartSpawning()
    {
        StartCoroutine(nameof(SpawnCubes));
    }
    private IEnumerator SpawnCubes()
    {
        yield return new WaitForSeconds(spawnTimer);
        while (true)
        {
            InstantiateTube();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
    private void Update()
    {
        float time = Time.time;
        minSpawnRangeTopY = Mathf.Abs(amplitude * Mathf.Sin(2f * Mathf.PI * frequency * time))+threshHold;
        minSpawnRangeBotY = Mathf.Abs(amplitude * Mathf.Sin(2f * Mathf.PI * frequency* time + Mathf.PI / 2f)) +threshHold;

    }
    private void InstantiateTube()
    {
        GameObject obj = ObjectPooler.Instance.GetObjectFromPool("Tube");
        Quaternion quat = Quaternion.Euler(0f, 0f, 180f);
        obj.transform.position = UpVector();
        obj.transform.rotation = quat;
        obj.SetActive(true);
        ObjectPooler.Instance.ReturnObjectToPool("Tube", obj, returnTimer);

        Quaternion quat2 = Quaternion.Euler(0f, 0f, 0f);
        GameObject obj2 = ObjectPooler.Instance.GetObjectFromPool("Tube");
        obj2.transform.position = DownVector();
        obj2.transform.rotation = quat2;
        obj2.SetActive(true);
        ObjectPooler.Instance.ReturnObjectToPool("Tube", obj2, returnTimer);


    }
    private Vector3 UpVector() => Vector3.up * (Random.Range(minSpawnRangeTopY, maxSpawnRangeTopY)) + Vector3.right * (distance + playerTransform.position.x);
    private Vector3 DownVector() => Vector3.up * (Random.Range(-minSpawnRangeBotY, -maxSpawnRangeBotY)) + Vector3.right * (distance + playerTransform.position.x);

}
