using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _05CubeSpawner : MonoBehaviour
{
    public static _05CubeSpawner Instance;
    private int objIndex = 0;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        GameObject platform = SpawnPlatform(new Vector3(3f, 0.55f, 0f), new Quaternion(0, -0.707105756f, 0, 0.707107902f));

    }

    public GameObject SpawnPlatform(Vector3 pos, Quaternion rotation)
    {
        GameObject platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // float posX = _05MovingPlatform.CurrentPlatform ? _05MovingPlatform.CurrentPlatform.transform.position.z : 3f;
        // float posY = _05MovingPlatform.CurrentPlatform ? _05MovingPlatform.CurrentPlatform.transform.position.y + 0.1f : 0.55f;
        // float posZ = _05MovingPlatform.CurrentPlatform ? _05MovingPlatform.CurrentPlatform.transform.position.x : 0f;
        // platform.transform.position = new Vector3(posX, posY, posZ);
        platform.transform.position = pos;
        _05MovingPlatform current = _05MovingPlatform.CurrentPlatform;
        if (current)
        {
            float posX = current.transform.position.x;
            float posZ = current.transform.position.z;
            platform.transform.position += new Vector3(posX, 0, posZ);
        }
        platform.transform.rotation = rotation;
        float scaleX = current ? current.transform.localScale.z : 1f;
        float scaleZ = current ? current.transform.localScale.x : 1f;
        platform.transform.localScale = new Vector3(scaleX, 0.1f, scaleZ);
        platform.gameObject.name = (++objIndex).ToString() + "_Platform";
        _05MovingPlatform.LastPlatform = current;
        _05MovingPlatform.CurrentPlatform = platform.AddComponent<_05MovingPlatform>();


        return platform;
    }
}
