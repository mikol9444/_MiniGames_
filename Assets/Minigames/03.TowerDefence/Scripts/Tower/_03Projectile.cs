using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials;
public interface _03_IDamager
{
    public float DamageAmount { get; set; }
}

public class _03Projectile : MonoBehaviour,_03_IDamager
{
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float damageAmount = 15f;
    public float DamageAmount { get => damageAmount; set => damageAmount=value; }

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

