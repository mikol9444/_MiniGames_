using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Playables;
using System.Linq;
public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}
    public enum PoolType
    {
        ParticleSystem,
        GameObject,
        None
    }
public class ObjectPoolManager : MonoBehaviour
{

    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    private GameObject _objectPoolEmptyHolder;
    private static GameObject _particleSystemsEmpty;
    private static GameObject _gameObjectsEmpty;


    private void Awake()
    {
        _objectPoolEmptyHolder = new GameObject("Pooled Objects");
        _particleSystemsEmpty = new GameObject("Particle Effects");
        _particleSystemsEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _gameObjectsEmpty = new GameObject("GameObjects");
        _gameObjectsEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);
    }
    public static PoolType PoolingType;
    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name); //Same functionality as code below (Lamda expression)

        // PooledObjectInfo pool = null;
        // foreach (PooledObjectInfo p in ObjectPools)
        // {
        //     if(p.LookupString == objectToSpawn.name){
        //         pool = p;
        //         break;
        //     }
        // }

        //if the pool doesnt exist, create it 
        if (pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();

        // GameObject spawnableObj = null;
        // foreach (GameObject obj in pool.InactiveObjects)
        // {
        //     if(obj !=null){
        //         spawnableObj = obj;
        //         break;
        //     }
        // }

        if (spawnableObj == null)
        {
            //Find the parent of the empty object
            GameObject parentObject = SetParentObject(poolType);

            //if there are no inactivate objects, create a new one 
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);

            if (parentObject != null)
            {
                spawnableObj.transform.SetParent(parentObject.transform);
            }
        }
        else
        {
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }
        return spawnableObj;

    }
    public static GameObject SpawnObject(GameObject objectToSpawn, Transform parentTransform)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);
        //if the pool doesnt exist, create it 
        if (pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();
        if (spawnableObj == null)
        {
            //if there are no inactivate objects, create a new one 
            spawnableObj = Instantiate(objectToSpawn, parentTransform);
        }
        else
        {
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }
        return spawnableObj;

    }
    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7);
        // PooledObjectInfo pool = null;
        // foreach (PooledObjectInfo p in ObjectPools)
        // {
        //     if (p.LookupString == goName)
        //     {
        //         Debug.Log(p.LookupString + "==" + goName);
        //         pool = p;
        //         break;
        //     }
        // }
         PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);

        if (pool == null)
        {
            Debug.LogWarning("Trying to release an boject that is not pooled:" + goName);
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }

    private static GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.ParticleSystem:
                return _particleSystemsEmpty;

            case PoolType.GameObject:
                return _gameObjectsEmpty;

            case PoolType.None:
                return null;
            default:
                return null;
        }
    }

}

