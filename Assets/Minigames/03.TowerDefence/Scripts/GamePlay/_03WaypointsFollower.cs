using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _03WaypointsFollower : MonoBehaviour
{
    [SerializeField] private Color sphereColor = Color.cyan;
    [SerializeField] private float sphereRadius = 1.5f;
    [SerializeField] private float movementSpeed = 1.5f;
    [SerializeField] private float distanceThreshold = 0.25f;
    [SerializeField] private int currentWaypointIndex = 0;
    [SerializeField] private bool drawGizmo;

    private _03Waypoints waypointsScript;
    private Transform targetTransform;

    private void Start()
    {
        waypointsScript = FindObjectOfType<_03Waypoints>();
        if (!waypointsScript)
        {
            Debug.LogWarning($"Waypoints not found in {gameObject.name}, going to sleep now -.-");
            gameObject.SetActive(false);
        }
        transform.position = waypointsScript.GetCurrentWaypoint(0);
    }
    private void OnEnable()
    {
        if (waypointsScript)
            transform.position = waypointsScript.GetCurrentWaypoint(0);
    }
    private void OnDisable()
    {
        currentWaypointIndex = 0;
    }
    private void Update()
    {
        if (waypointsScript)
        {
            // Move towards the current waypoint
            Vector3 targetPosition = waypointsScript.GetCurrentWaypoint(currentWaypointIndex);
            float distance = Vector3.Distance(transform.position, targetPosition);
            if (distance > distanceThreshold)
            {
                Vector3 direction = (targetPosition - transform.position).normalized;
                transform.position += direction * movementSpeed * Time.deltaTime;
            }
            else
            {
                // Move to the next waypoint when arriving at the current waypoint
                MoveNextWaypoint();
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (drawGizmo)
        {
            Gizmos.color = sphereColor;
            Gizmos.DrawSphere(transform.position, sphereRadius);
        }
    }
    public void MoveNextWaypoint()
    {
        currentWaypointIndex++;
        if (currentWaypointIndex >= waypointsScript.ChildLength)
        {

            Essentials.ObjectPooler.Instance.ReturnObjectToPool("WayPointsFollower", gameObject);
            //transform.position = waypointsScript.GetCurrentWaypoint(currentWaypointIndex);
        }
    }

}
