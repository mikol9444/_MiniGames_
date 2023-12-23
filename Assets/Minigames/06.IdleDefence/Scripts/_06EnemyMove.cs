using System.Collections;
using System.Collections.Generic;
using Essentials;
using UnityEngine;

public class _06EnemyMove : MonoBehaviour
{
    public string playerTag = "Player";
    public float movementSpeed = 5f;
    public float deactivateThreshold = 1f; // Adjust this threshold as needed

    private Transform playerTransform;

    void Start()
    {
        // Search for the player's transform with the specified tag
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found with tag: " + playerTag);
        }
    }

    void Update()
    {
        // Move towards the player
        if (playerTransform != null)
        {
            MoveTowardsPlayer();

            // Check distance to player and deactivate if within threshold
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= deactivateThreshold)
            {
                // Optionally, you might want to play an explosion effect or trigger other actions
                // before deactivating the enemy
                DeactivateEnemy();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);

        // Optionally, you can use LookAt to make the enemy face the player while moving
        transform.LookAt(playerTransform);
    }

    public void DeactivateEnemy()
    {
        gameObject.SetActive(false);
        // Optionally, you might want to reset or do other cleanup before deactivating
        FindObjectOfType<_06PlayerShoot>().RemoveEnemy(gameObject);
        ObjectPooler.Instance.ReturnObjectToPool("Enemy",gameObject);
    }
}
