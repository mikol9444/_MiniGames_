using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _03TowerScript : MonoBehaviour
{

    public enum TowerType { Ballistic }

    public TowerType towerType;
    public LayerMask enemyMask;
    public float startDamage = 5f;
    public float startRange = 10f;
    public float scanInterval = 0.5f;
    public int startCost = 10;
    public bool isInitialized = false;
    protected Transform target;
    [SerializeField] protected Transform shootingPoint;

    protected Animator anim;
    // Add a method to upgrade the tower
    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected virtual void OnEnable()
    {
        if (!isInitialized)
        {
            Debug.LogWarning($"WARNING: TOWER {this.gameObject.name} is ready!");
            isInitialized = true;
        }
    }
    protected virtual void OnDisable()
    {
        if (isInitialized)
        {
            Debug.LogWarning($"TOWER {this.gameObject.name} is Destroyed -.- ");
            isInitialized = false;
        }
    }
    // Add a method to attack enemies
    public virtual void Attack()
    {

        // Check if the target is within range
        //float distance = Vector3.Distance(transform.position, target.transform.position);
        //Debug.Log($"Distance to {target.name} is {distance}");
        //if (distance <= range)
        //{
        //    // Attack the target
        //    EnemyScript enemy = target.GetComponent<EnemyScript>();
        //    if (enemy != null)
        //    {
        //        enemy.TakeDamage(damage);
        //    }
        //}
    }
    //protected virtual void Kaputt()
    //{
    //    anim.SetTrigger("kaputt");
    //}

}
