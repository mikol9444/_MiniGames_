using System.Collections;
using System.Collections.Generic;
using Essentials;
using UnityEngine;
using TMPro;

public class ExampleSpawnerScript : MonoBehaviour
{
    public float particlesPerSecond = 5f;

    [SerializeField] private string objectToSpawnName = "Particle";

    private float waitTime;
    private float cooldown = 2f;
    private Vector3 offset;
    private float randValue;
    private void Start()
    {
        offset = transform.position;
        StartCoroutine(ShootParticles());

    }

    private IEnumerator ShootParticles()
    {
        while (gameObject.activeInHierarchy)
        {
            GameObject obj = ObjectPooler.Instance.GetObjectFromPool(objectToSpawnName);
            if (!obj)
            {
                yield return new WaitForSeconds(cooldown);
                continue;
            }
            obj.transform.position = transform.position;
            yield return new WaitForSeconds(waitTime);
            waitTime = 1f / particlesPerSecond;
        }
    }


}
