using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootUpObject : MonoBehaviour
{
    public float minForce = 10f;
    public float maxForce = 20f;
    public float deactivateTimer = 3f;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }
    private void OnEnable()
    {

        StartCoroutine(DeactivateSelf());
        ShootObjectUp();
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        rb.velocity = Vector3.zero;
    }

    private void ShootObjectUp()
    {
        // Generate a random direction for the force
        Vector3 forceDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(0.5f, 1f), Random.Range(-1f, 1f)).normalized;

        // Generate a random force magnitude
        float forceMagnitude = Random.Range(minForce, maxForce);

        // Apply the force to the Rigidbody
        rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
    }
    IEnumerator DeactivateSelf()
    {
        yield return new WaitForSeconds(deactivateTimer);
        gameObject.SetActive(false);
    }
}
