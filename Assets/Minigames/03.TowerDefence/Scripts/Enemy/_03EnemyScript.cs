using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials;
public interface _03_IDamagable
{
    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public void TakeDamage(float damageAmount);

}
public class _03EnemyScript : MonoBehaviour, _03_IDamagable
{
    [SerializeField]private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 100f;
    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (value <= 0f) { currentHealth = 0; ObjectPooler.Instance.ReturnObjectToPool("projectile", gameObject); currentHealth = 0; }
            else currentHealth = value;
        }
    }
    private void OnEnable()
    {
        currentHealth = maxHealth;
    }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    public void TakeDamage(float damageAmount) => CurrentHealth -= damageAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out _03_IDamager dmg))
        {
            CurrentHealth -= dmg.DamageAmount;
            Debug.Log($"ME ENEMY:{gameObject.name} have {CurrentHealth} left");
        }
    }
}
