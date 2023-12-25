using System.Collections;
using UnityEngine;

public class _00_ExampleSpawner : MonoBehaviour
{

    [SerializeField]
    private float cooldown = 2f;
   [SerializeField] private GameObject objectToSpawn;

    private void Start()
    {


        // Start spawning objects repeatedly
        StartCoroutine(SpawnObjectsRepeatedly());
    }

    private IEnumerator SpawnObjectsRepeatedly()
    {
        while (true)
        {
            // Spawn an object from the object pool
            GameObject spawnedObject = ObjectPoolManager.SpawnObject(objectToSpawn,transform.position,Quaternion.identity,PoolType.GameObject);

            if (spawnedObject != null)
            {
                // Set the position of the spawned object (you may want to customize this based on your game)
                spawnedObject.transform.position = transform.position;

                // Optionally, you can perform additional setup for the spawned object here
            }

            yield return new WaitForSeconds(cooldown);
        }
    }
}
