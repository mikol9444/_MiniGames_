using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Essentials
{

    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int poolSize;

        private Queue<GameObject> objectPool = new Queue<GameObject>();

        public void Initialize()
        {
            GameObject parent = new GameObject("Pool Parent");
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = UnityEngine.Object.Instantiate(prefab, parent.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
        }

        public GameObject GetObject()
        {
            if (objectPool.Count == 0)
            {
                Debug.LogWarning("Object pool is empty. Consider increasing pool size.");
                return null;
            }

            GameObject obj = objectPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        public void ReturnObject(GameObject obj)
        {
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }


}