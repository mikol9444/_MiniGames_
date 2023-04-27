using System.Collections;
using System.Collections.Generic;
using Essentials;
using UnityEngine;

public class ExampleParticle : MonoBehaviour
{
    // Start is called before the first frame update
    public float RecycleTimer = 3f;
    public float minPoint = .05f;
    public float maxPoint = .25f;

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
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void ReturnToPool()
    {
        ObjectPooler.Instance.ReturnObjectToPool("Particle", gameObject);
    }
}
