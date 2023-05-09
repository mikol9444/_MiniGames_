using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _03Ballista : _03TowerScript
{
    [SerializeField] private Transform canonTransform;
    [SerializeField] private bool drawRange;
    public override void Attack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, startRange, enemyMask);

        _03_WaypointsFollower closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        // Loop through all detected colliders and find closest enemy
        foreach (Collider collider in colliders)
        {
            _03_WaypointsFollower enemy = collider.GetComponent<_03_WaypointsFollower>();
            if (enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestEnemy = enemy;
                    closestDistance = distanceToEnemy;
                }
            }
        }

        // Look at closest enemy if one is found
        if (closestEnemy != null)
        {
            canonTransform.LookAt(closestEnemy.transform);
        }
    }

    private void Update()
    {
        Attack();
    }
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }
    private void OnDrawGizmos()
    {
        if (drawRange)
        {
            Gizmos.color = new Color(0f, 0.35f, 0f, 0.35f);
            Gizmos.DrawSphere(transform.position, startRange);
        }

    }
}
