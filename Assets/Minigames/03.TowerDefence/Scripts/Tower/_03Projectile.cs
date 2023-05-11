using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials;
public class _03Projectile : MonoBehaviour
{
    public float lifeTime = 3f;
    private void OnEnable()
    {
        Invoke(nameof(TurnOFf), lifeTime);
    }
    private void OnDisable()
    {
        CancelInvoke(nameof(TurnOFf));
        if(gameObject.TryGetComponent(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
        }
    }
    private void TurnOFf() => ObjectPooler.Instance.ReturnObjectToPool("projectile",gameObject);
}

