using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _11_Spawner : MonoBehaviour
{
    public GameObject carPrefab;
    public GameObject carPrefab2;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCar01), 0f, 3f);
        InvokeRepeating(nameof(SpawnCar02), 1f, 3f);
        InvokeRepeating(nameof(SpawnCar03), 1.5f, 3f);
                InvokeRepeating(nameof(SpawnCar04), 1.27f, 3f);
                        InvokeRepeating(nameof(SpawnCar05), 0.7f, 3f);
    }
    private void SpawnCar01()
    {
        ObjectPoolManager.SpawnObject(carPrefab, new Vector3(14, 0, 1), Quaternion.Euler(0f, -90f, 0f), PoolType.GameObject);
    }
    private void SpawnCar02()
    {
        ObjectPoolManager.SpawnObject(carPrefab, new Vector3(14, 0, 3), Quaternion.Euler(0f, -90f, 0f), PoolType.GameObject);
    }
    private void SpawnCar03()
    {
        ObjectPoolManager.SpawnObject(carPrefab, new Vector3(14, 0, 5), Quaternion.Euler(0f, -90f, 0f), PoolType.GameObject);
    }
        private void SpawnCar04()
    {
        ObjectPoolManager.SpawnObject(carPrefab2, new Vector3(-5, 0, 2), Quaternion.Euler(0f, 90f, 0f), PoolType.GameObject);
    }
        private void SpawnCar05()
    {
        ObjectPoolManager.SpawnObject(carPrefab2, new Vector3(-5, 0, 4), Quaternion.Euler(0f, 90f, 0f), PoolType.GameObject);
    }
}
