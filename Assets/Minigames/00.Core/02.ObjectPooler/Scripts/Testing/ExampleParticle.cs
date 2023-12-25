using System.Collections;
using System.Collections.Generic;
using Essentials;
using UnityEngine;

public class ExampleParticle : MonoBehaviour
{
    public string poolName = "BlackHole";
    // Start is called before the first frame update
    public float RecycleTimer = 3f;
    public float minPoint = .05f;
    public float maxPoint = .25f;
    private Rigidbody rb;

    private void Awake()
    {
           rb = GetComponent<Rigidbody>();
    }
    void OnEnable()
    {
        Invoke(nameof(ReturnToPool), RecycleTimer);
        float value = Random.Range(minPoint, maxPoint);
        Vector3 randomPoint = new Vector3(
            Random.Range(value, value),
            Random.Range(value, value),
            Random.Range(value, value)
        );
        transform.localScale = randomPoint;
        // Shoot the Rigidbody up
        rb.velocity = Vector3.up * 10f;
        // Randomly change the Rigidbody's velocity to fly in different directions
        rb.velocity = new Vector3(Random.Range(-5f, 5f), Random.Range(0f, 5f), Random.Range(-5f, 5f));
    }
    private void OnDisable()
    {
        CancelInvoke();
        rb.velocity = Vector3.zero;
    }
    private void ReturnToPool()
    {
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
