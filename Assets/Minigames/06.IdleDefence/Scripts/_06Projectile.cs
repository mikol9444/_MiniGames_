using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _06Projectile : MonoBehaviour
{
    public GameObject enemy;
    public float speed;
    public _06PlayerShoot enemyList;
    private void Start() {
        enemyList = FindObjectOfType<_06PlayerShoot>();
    }
void Update()
    {
        if (enemyList.enemiesInRange.Count>0)
        {
            enemy = enemyList.enemiesInRange[0];
            if(!enemy) enemyList.RemoveEnemy(enemyList.enemiesInRange[0]);
        }
        if (enemy != null)
        {
            // Move towards the enemy
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Compare the tag of the collided object with "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Deactivate both the projectile and the enemy
            // collision.gameObject.SetActive(false);
            FindObjectOfType<_06PlayerShoot>().RemoveEnemy(collision.gameObject);
            Destroy(collision.gameObject);
            Destroy(gameObject) ;
            _06_EnemySpawner spawner = FindObjectOfType<_06_EnemySpawner>();
            spawner.enemyCount--;
            spawner.textMesh.text = spawner.enemyCount.ToString();
        }
    }
}
