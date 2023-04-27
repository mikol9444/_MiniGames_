using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Essentials
{
    [System.Serializable]
    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler Instance;
        [SerializeField] private Pool[] pools;

        private Dictionary<string, Pool> poolDictionary = new Dictionary<string, Pool>();

        private void Awake()
        {
            foreach (Pool pool in pools)
            {
                pool.Initialize();
                poolDictionary.Add(pool.name, pool);
            }
            if (Instance != this) Destroy(gameObject);
            Instance = this;
        }
        private void Onen()
        {
            poolDictionary.Clear();
        }

        public GameObject GetObjectFromPool(string poolName)
        {
            if (!poolDictionary.ContainsKey(poolName))
            {
                Debug.LogWarning("Pool with name " + poolName + " does not exist.");
                return null;
            }
            GameObject obj = poolDictionary[poolName].GetObject();

            return obj;
        }

        public void ReturnObjectToPool(string poolName, GameObject obj)
        {
            if (!poolDictionary.ContainsKey(poolName))
            {
                Debug.LogWarning("Pool with name " + poolName + " does not exist.");
                return;
            }

            poolDictionary[poolName].ReturnObject(obj);
        }
    }
}
