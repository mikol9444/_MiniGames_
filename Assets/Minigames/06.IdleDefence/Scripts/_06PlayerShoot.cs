using System.Collections;
using System.Collections.Generic;
using Essentials;
using UnityEngine;

public class _06PlayerShoot : MonoBehaviour
{
    public List<GameObject> enemiesInRange;
    public Transform shootingPoint;
    public GameObject projectilePrefab;
    public float attackSpeed = 1f; // Adjust this as needed
    private bool canAttack = true;
    private void Awake() {
        enemiesInRange = new List<GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    void Update()
    {
        if (canAttack && enemiesInRange.Count > 0)
        {
            AttackEnemy();
        }
    }

    void AttackEnemy()
    {
        // Assuming you want to attack the first enemy in the list
        if(enemiesInRange.Count>0){
        GameObject enemyToAttack = enemiesInRange[0];
        if (enemyToAttack)transform.LookAt(enemyToAttack.transform);
        GetComponent<Animator>().SetTrigger("throw");
        // Set a cooldown before the next attack
        StartCoroutine(AttackCooldown());
        }

    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(1 / attackSpeed); // Calculate cooldown based on attack speed
        canAttack = true;
    }

    void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab,shootingPoint.position,Quaternion.identity);
        // projectile.transform.position = shootingPoint.position;
        projectile.SetActive(true);
    }
}
