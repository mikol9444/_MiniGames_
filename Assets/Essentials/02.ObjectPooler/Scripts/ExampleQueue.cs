using System.Collections.Generic;
using UnityEngine;

public class ExampleQueue : MonoBehaviour
{
    private Queue<GameObject> objectQueue = new Queue<GameObject>();
    public GameObject prefab;

    private void Start()
    {
        // Add three instances of the prefab to the queue
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = Instantiate(prefab);
            objectQueue.Enqueue(obj);
        }

        // Print the number of elements in the queue
        Debug.Log("Queue size: " + objectQueue.Count);

        // Remove the first element from the queue and activate it
        GameObject firstObj = objectQueue.Dequeue();
        firstObj.SetActive(true);

        // Print the number of elements in the queue
        Debug.Log("Queue size: " + objectQueue.Count);

        // Add a new instance of the prefab to the queue
        GameObject newObj = Instantiate(prefab);
        objectQueue.Enqueue(newObj);

        // Print the number of elements in the queue
        Debug.Log("Queue size: " + objectQueue.Count);
    }
}
