using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _03Waypoints : MonoBehaviour
{
    [SerializeField] private bool showGizmos = true;
    [SerializeField] private GameObject portalPrefab;
    private Transform[] childTransforms;
    private LineRenderer lineRend;
    public int ChildLength { get => childTransforms.Length; }
    private void Awake()
    {
        GetAllChildren();
        lineRend = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        portalPrefab.transform.position = GetCurrentWaypoint(0);
    }
    private void OnValidate()
    {
        if (childTransforms == null) return;
        if (!showGizmos)
        {
            foreach (var item in childTransforms)
            {
                item.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (var item in childTransforms)
            {
                item.gameObject.SetActive(true);
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            GetAllChildren();
            // Draw a green cube at the position of each child GameObject
            Gizmos.color = Color.green;
            foreach (Transform childTransform in childTransforms)
            {
                Gizmos.DrawCube(childTransform.position, Vector3.one);
            }

            // Draw lines connecting each child GameObject
            Gizmos.color = Color.blue;
            for (int i = 0; i < childTransforms.Length - 1; i++)
            {
                Gizmos.DrawLine(childTransforms[i].position, childTransforms[i + 1].position);
            }
        }
    }
    private void GetAllChildren()
    {
        int childCount = transform.childCount;
        // Get the transforms of all child GameObjects
        childTransforms = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            childTransforms[i] = transform.GetChild(i);
        }
        //Set Up Line Renderer positions 
        if (gameObject.TryGetComponent(out lineRend))
        {
            lineRend.positionCount = childCount;
            for (int i = 0; i < childCount; i++)
            {
                lineRend.SetPosition(i, childTransforms[i].position);
            }
        }

    }


    public Vector3 GetCurrentWaypoint(int currentWaypointIndex)
    {
        if (childTransforms.Length == 0)
        {
            return transform.position;
        }

        return childTransforms[currentWaypointIndex].position;
    }



}
