using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials;
using UnityEngine.UI;
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
    [SerializeField] private GameObject sliderCanvas;
    private Slider healthSlider;

    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (value <= 0f) { currentHealth = 0; ObjectPooler.Instance.ReturnObjectToPool("projectile", gameObject); currentHealth = 0; }
            else currentHealth = value;
        }
    }
    private void Awake()
    {
        healthSlider = sliderCanvas.GetComponentInChildren<Slider>();
        //healthSlider.maxValue = maxHealth;
    }
    private void OnEnable()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    public void TakeDamage(float damageAmount)
    {
        CancelInvoke(nameof(TurnOffSlider));
        sliderCanvas.gameObject.SetActive(true);
        Invoke(nameof(TurnOffSlider), 3f);
        healthSlider.value = currentHealth;
        CurrentHealth -= damageAmount;
        
    }
    private void TurnOffSlider()
    {
        sliderCanvas.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out _03_IDamager dmg))
        {
            TakeDamage(dmg.DamageAmount);
            dmg.TurnOff();
            Debug.Log($"ME ENEMY:{gameObject.name} have {CurrentHealth} left");
        }
    }
}
