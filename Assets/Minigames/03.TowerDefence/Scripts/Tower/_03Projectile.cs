using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials;
public interface _03_IDamager
{
    public float DamageAmount { get; set; }
    public Transform Target { get; set; }
    public void TurnOff();
}

public class _03Projectile : MonoBehaviour,_03_IDamager
{
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float damageAmount = 15f;
    public float DamageAmount { get => damageAmount; set => damageAmount=value; }
    private Transform target; 
    public Transform Target { get => target; set => target=value; }

    private Rigidbody rb;
    private float projectileSpeed = 15f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (Target)
        {
            Vector3 direction = target.position - transform.position;
            rb.velocity = direction.normalized * projectileSpeed;
        }
    }
    private void OnEnable()
    {
        Invoke(nameof(TurnOff), lifeTime);
    }
    private void OnDisable()
    {
        CancelInvoke(nameof(TurnOff));
        if(gameObject.TryGetComponent(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
        }
        target = null;
        projectileSpeed = 0f;
    }
    public void TurnOff() => ObjectPooler.Instance.ReturnObjectToPool("projectile",gameObject);

    public void NavigateTowardsEnemy(Transform target, float projectileSpeed)
    {
        this.target = target;
        this.projectileSpeed = projectileSpeed;

        AudioManager_Test.Instance.PlaySound("Shoot");
    }


}

