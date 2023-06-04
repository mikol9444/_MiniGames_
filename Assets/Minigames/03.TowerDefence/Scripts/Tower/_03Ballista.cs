using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _03Ballista : _03TowerScript
{
    [SerializeField] private Transform canonTransform;
    [SerializeField] private bool drawGizmos;
    [SerializeField] private float projectileSpeed = 25f;
    public float rotationTime = 1f;
    public _03Projectile projectilePrefab;
    public float thresholdAngle = 45f;

    void Start()
    {
        //StartCoroutine(ScanForEnemies());
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
        if (drawGizmos)
        {
            Gizmos.color = new Color(0f, 0.35f, 0f, 0.35f);
            Gizmos.DrawSphere(transform.position, startRange);
        }

    }
    IEnumerator ScanForEnemies()
    {
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, startRange);
            float closestDistance = float.MaxValue;
            Transform closestTarget = null;

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTarget = collider.transform;
                    }
                }
            }

            if (closestTarget != null)
            {
                target = closestTarget;
                StartCoroutine(RotateTowardsTarget());
            }
            else
            {
                target = null;
            }

            yield return new WaitForSeconds(scanInterval);
        }
    }

    IEnumerator RotateTowardsTarget()
    {
        Vector3 targetDirection = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion startRotation = canonTransform.rotation;
        float elapsedTime = 0f;
        float angle = Quaternion.Angle(startRotation, targetRotation);

        while (elapsedTime < rotationTime)
        {
            canonTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationTime);
            elapsedTime += Time.deltaTime;
            angle = Quaternion.Angle(canonTransform.rotation, targetRotation);
            yield return null;
        }

        if (angle < thresholdAngle)
        {
            Shoot();
        }
    }



    void Shoot()
    {
        if (projectilePrefab != null && target != null)
        {
            _03Projectile projectile = Essentials.ObjectPooler.Instance?.GetObjectFromPool("projectile")?.GetComponent<_03Projectile>();
            projectile?.transform.SetPositionAndRotation(shootingPoint.position, Quaternion.identity);
            //= Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile?.NavigateTowardsEnemy(target,projectileSpeed);
        }
    }
    public void Kaputt()
    {
        if (gameObject.activeInHierarchy)
        {
            anim.SetTrigger("kaputt");
            StopAllCoroutines();
        }

    }
    public void Spawn()
    {

            
            StartCoroutine(ScanForEnemies());
        

    }
    public void Deactivate() => gameObject.SetActive(false);
}
