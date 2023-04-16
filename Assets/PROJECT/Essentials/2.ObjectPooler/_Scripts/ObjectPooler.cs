using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public Pool[] pools;
    // Dictionary to store the poolName and corresponding pool index
    private Dictionary<string, int> poolDictionary = new Dictionary<string, int>();
    public static ObjectPooler Instance;
    private Transform[] parentObjects;
    private void Awake()
    {
        Initialize();
        Instance = this;

    }
    public void TestFunction(bool status)
    {
    }

    private void Initialize()
    {
        // PREINITIALIZE Prefab amount from each pool and Add the pool name and index to the dictionary
        int index = 0;
        parentObjects = new Transform[pools.Length];
        foreach (Pool pool in pools)
        {
            for (int i = 0; i < pool.preinstantiateAmount; i++)
            {
                GameObject obj = InitObject(pool, false);
            }
            // 

            poolDictionary.Add(pool.poolName, index++);

            GameObject emptyObject = new GameObject(pool.poolName);
            emptyObject.transform.parent = transform;
            pool.parentTransform = emptyObject.transform;
        }


    }

    private Pool SelectPool(string poolName)
    {
        int index = poolDictionary.GetValueOrDefault(poolName);
        //Debug.Log($"Selected pool index: {index} input name {poolName} pool name {pools[index].poolName} ");
        return pools[index];
    }
    private GameObject InitObject(Pool pool, bool state)
    {
        if (pool.CanPull)
        {
            GameObject obj = Instantiate(pool.prefab, pool.parentTransform);
            pool.AddObject(obj);
            obj.SetActive(state);
            return obj;
        }
        return null;
    }
    /// <summary>
    ///Return or Instantiates first inactive Object in Pool , you have still to set up Vector3 Coordinates manually"
    /// </summary>
    public GameObject PoolObject(string poolName)
    {
        Pool pool = SelectPool(poolName);
        GameObject obj = pool.FirstInactive(pool);
        if (obj == null) obj = InitObject(pool, true);
        return obj;
    }


}