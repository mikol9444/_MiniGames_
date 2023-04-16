using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSpawner : MonoBehaviour
{

    ObjectPooler pooler;
    [SerializeField] private float timeStep;
    [SerializeField] private string spawnPool = "default";
    public static testSpawner Instance;

    private void Start()
    {
        pooler = ObjectPooler.Instance;
        StartCoroutine(SpawnContinue(timeStep, spawnPool));
        Instance = this;
    }
    public bool SpawnObjectFromPool(string poolName, Vector3 spawnposition = default(Vector3), Quaternion rotation = default(Quaternion), Transform parent = default(Transform))
    {
        GameObject obj = pooler.PoolObject(poolName); ;
        if (obj)
        {
            obj.transform.position = spawnposition;
            obj.transform.rotation = rotation;
            obj.transform.parent = parent;
            return true;
        }
        return false;

    }
    public IEnumerator SpawnContinue(float timeStep, string poolName)
    {
        while (true)
        {
            Vector3 randomPos = transform.position;
            SpawnObjectFromPool(poolName, randomPos);
            yield return new WaitForSeconds(timeStep);
        }
    }
}
