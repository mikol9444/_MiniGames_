using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Essentials
{
    /// <summary>
    /// GetObject from Pool or return it. Each pool is working with Queue <para>
    /// To add more pools create additional placeholder class for a pool child </para>
    ///and Initialize it with InitializePools method
    /// </summary>
    [System.Serializable]
    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler Instance;
        //Playceholder child classes to devide pools in categories
        [System.Serializable]
        public class Pool_01 : Pool { }
        [System.Serializable]
        public class Pool_02 : Pool { }
        [System.Serializable]
        public class Pool_03 : Pool { }

        [SerializeField] private Pool_01[] particles;
        [SerializeField] private Pool_02[] projectiles;
        [SerializeField] private Pool_03[] enemies;

        private Dictionary<string, Pool> poolDictionary = new Dictionary<string, Pool>();
        //Singleton
        private void Awake()
        {
            if (Instance != this) Destroy(gameObject);
            Instance = this;

            InitializePools(particles);
            InitializePools(projectiles);
            InitializePools(enemies);
        }
        private void InitializePools(Pool[] pools)
        {
            foreach (Pool pool in pools)
            {
                pool.Initialize();
                poolDictionary.Add(pool.name, pool);
            }
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
