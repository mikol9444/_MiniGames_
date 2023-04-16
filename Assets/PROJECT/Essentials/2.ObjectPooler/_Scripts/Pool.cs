using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class Pool
{
    public string poolName = "default";
    public GameObject prefab;
    public int maxPoolSize = 25;
    public int preinstantiateAmount = 10;
    public bool expandable = false;
    private List<GameObject> pooledObjects = new List<GameObject>();
    [HideInInspector] public Transform parentTransform;

    public int Count { get => pooledObjects.Count; }
    public bool CanPull { get { return (Count < maxPoolSize || expandable); } }
    public void AddObject(GameObject obj) => pooledObjects.Add(obj);
    public GameObject FirstInactive(Pool pool)
    {
        for (int i = 0; i < pool.Count; i++)
        {

            if (!pool.pooledObjects[i].activeInHierarchy)
            {
                pool.pooledObjects[i].SetActive(true);
                return pool.pooledObjects[i];
            }
        }
        return null;
    }
}


