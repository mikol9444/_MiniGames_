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


        [SerializeField] private Pool[] particles;
        [SerializeField] private Pool[] projectiles;
        [SerializeField] private Pool[] enemies;

        private Dictionary<string, Pool> poolDictionary = new Dictionary<string, Pool>();
        //Singleton
        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
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
        public void ReturnObjectToPool(string poolName, GameObject obj,float time)
        {
            StartCoroutine(ReturnObjectAfterTime(poolName, obj, time));
        }
        private IEnumerator ReturnObjectAfterTime(string poolName, GameObject obj, float time)
        {
            yield return new WaitForSeconds(time);
            if (!poolDictionary.ContainsKey(poolName))
            {
                Debug.LogWarning("Pool with name " + poolName + " does not exist.");
                yield return null;
            }

            poolDictionary[poolName].ReturnObject(obj);
        }
    }
}
