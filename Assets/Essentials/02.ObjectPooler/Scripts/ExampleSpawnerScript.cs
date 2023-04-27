using System.Collections;
using System.Collections.Generic;
using Essentials;
using UnityEngine;

public class ExampleSpawnerScript : MonoBehaviour
{
    public float particlesPerSecond = 5f;
    [SerializeField] private float radius = 2.0f;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float spiralFactor = 1.0f;

    private float waitTime;
    private float cooldown = 2f;
    private Vector3 offset;
    private float randValue;
    private void Start()
    {
        offset = transform.position;
        StartCoroutine(ShootParticles());
    }
    private void Update()
    {
        transform.position = RocketMovement() + offset;

    }
    private Vector3 RocketMovement()
    {
        float angle = Time.time * speed;
        float x = Mathf.Sin(angle) * radius;
        float z = Mathf.Cos(angle) * radius;
        return new Vector3(x, 0, z);
    }
    private Vector3 SprialMovement()
    {
        float angle = Time.time * speed;
        float x = Mathf.Cos(angle) * (radius + angle * spiralFactor);
        float y = Mathf.Sin(angle) * (radius + angle * spiralFactor);
        float z = Mathf.Sin(angle) * (radius + angle * spiralFactor);
        return new Vector3(x, y, z);
    }
    private Vector3 EightMovement()
    {
        float angle = Time.time * speed;
        float x = Mathf.Sin(angle) * radius;
        float y = Mathf.Cos(angle * 2) * radius;
        float z = Mathf.Sin(angle * 3) * radius;
        return new Vector3(x, y, z);
    }
    private IEnumerator ShootParticles()
    {
        while (gameObject.activeInHierarchy)
        {
            GameObject obj = ObjectPooler.Instance.GetObjectFromPool("Particle");
            if (!obj)
            {
                yield return new WaitForSeconds(cooldown);
                continue;
            }
            // Rigidbody rb = obj.GetComponent<Rigidbody>();
            obj.transform.position = transform.position + Vector3.up;

            // // Shoot the Rigidbody up
            // rb.velocity = Vector3.up * 10f;


            // // Randomly change the Rigidbody's velocity to fly in different directions
            // rb.velocity = new Vector3(Random.Range(-5f, 5f), Random.Range(0f, 5f), Random.Range(-5f, 5f));
            // Wait for a short time
            yield return new WaitForSeconds(waitTime);
            waitTime = 1f / particlesPerSecond;
        }
    }


}
