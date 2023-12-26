using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _11_Spawner : MonoBehaviour
{
    public GameObject carPrefab;
    public GameObject playerPrefab;

    private void Start()
    {
        // InvokeRepeating(nameof(SpawnCar01), 0f, 3f);
        // InvokeRepeating(nameof(SpawnCar02), 1f, 3f);
        // InvokeRepeating(nameof(SpawnCar03), 1.5f, 3f);
        // InvokeRepeating(nameof(SpawnCar04), 1.27f, 3f);
        // InvokeRepeating(nameof(SpawnCar05), 0.7f, 3f);
        InvokeRepeating(nameof(SpawnManyCars), 0f, 3f);
    }
    private void SpawnManyCars()
    {
        Quaternion rot = Quaternion.Euler(0f, 90f, 0f);
        List<_11_SingleObject> cars = new List<_11_SingleObject>
        {
            SpawnCar(new Vector3(14, 0, 1), rot),
            SpawnCar(new Vector3(-5, 0, 2), rot),
            SpawnCar(new Vector3(15, 0, 3), rot),
            SpawnCar(new Vector3(-9, 0, 4), rot),
            SpawnCar(new Vector3(17, 0, 5), rot),


            SpawnCar(new Vector3(27, 0, 7), rot),
            SpawnCar(new Vector3(-7, 0, 8), rot),
            SpawnCar(new Vector3(17, 0, 9), rot),
            SpawnCar(new Vector3(-10, 0, 10), rot),
            SpawnCar(new Vector3(19, 0, 11), rot),

        };
        for (int i = 0; i < cars.Count; i++)
        {
            if (i % 2 == 1 && i <5)
            {
                cars[i].moveRight = true;
            }
                        if (i % 2 == 0 && i >=5)
            {
                cars[i].moveRight = true;
            }
            cars[i].moveSpeed += Random.Range(0, 2f);
        }
        // car.moveRight = true; car2.moveRight = true;
        // car.moveSpeed -= Random.Range(-1f, 1f);

    }
    private _11_SingleObject SpawnCar(Vector3 pos, Quaternion euler)
    {
        GameObject obj = ObjectPoolManager.SpawnObject(carPrefab, pos, euler, PoolType.GameObject);
        _11_SingleObject car = obj.GetComponent<_11_SingleObject>();
        return car;
    }

    public void SpawnPlayer(Vector3 pos, Quaternion euler){
        Instantiate(playerPrefab,pos,euler);
    }
    // private void SpawnCar01()
    // {
    //     ObjectPoolManager.SpawnObject(carPrefab, new Vector3(14, 0, 1), Quaternion.Euler(0f, -90f, 0f), PoolType.GameObject);
    // }
    // private void SpawnCar02()
    // {
    //     ObjectPoolManager.SpawnObject(carPrefab, new Vector3(14, 0, 3), Quaternion.Euler(0f, -90f, 0f), PoolType.GameObject);
    // }
    // private void SpawnCar03()
    // {
    //     ObjectPoolManager.SpawnObject(carPrefab, new Vector3(14, 0, 5), Quaternion.Euler(0f, -90f, 0f), PoolType.GameObject);
    // }
    //     private void SpawnCar04()
    // {
    //     ObjectPoolManager.SpawnObject(carPrefab, new Vector3(-5, 0, 2), Quaternion.Euler(0f, 90f, 0f), PoolType.GameObject);
    // }
    //     private void SpawnCar05()
    // {
    //     ObjectPoolManager.SpawnObject(carPrefab, new Vector3(-5, 0, 4), Quaternion.Euler(0f, 90f, 0f), PoolType.GameObject);
    // }
}
