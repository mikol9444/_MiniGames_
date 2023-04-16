using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner_test : MonoBehaviour
{
    public int currentPosition = -999;

    List<GameObject> objects = new List<GameObject>();
    private void Start()
    {

        for (int i = 0; i < 250; i++)
        {
            GameObject cube = ObjectPooler.Instance.PoolObject("Cube");
            objects.Add(cube);
        }
        int childrenLenghs = 0;
        foreach (var item in objects)
        {
            item.GetComponentInChildren<Collider>().gameObject.AddComponent<Jack_Detector>().Index = childrenLenghs++;
            item.SetActive(false);
        }
        SpawnCube(-1);
    }
    public void SpawnCube(int index)
    {
        // if (index >= 2)
        // {
        //     objects[index - 2].SetActive(false);
        // }
        GameObject cube = ObjectPooler.Instance.PoolObject("Cube");
        cube.transform.position = Vector3.right * (index + 1) + Vector3.down;
    }
}
