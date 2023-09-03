using System.Collections;
using System.Collections.Generic;
using Essentials;
using UnityEngine;
using UnityEditor;
public class _05GameManager01 : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab; // Reference to the platform prefab to instantiate
    public bool platformForward = true; // Global bool value for switching look direction
    private Vector3 spawnPosition = Vector3.zero; // Initial spawn position
    [SerializeField] private float height = 0.1f;
    [SerializeField] private _05MovingPlatform01 CurrentPlatform; // Reference to the current platform Gameobject to instantiate
    [SerializeField] private _05MovingPlatform01 LastPlatform;// Reference to the last platform Gameobject to instantiate
    [SerializeField] private float distanceToOrigin = 3f;
    [SerializeField] private float distanceThreshold = 0.5f;
    [SerializeField] private float applyDistance = 0.25f;

    [SerializeField] private float direction = 1f;
    [SerializeField] private Popup popup;


    public int PlatformCount = 0;
    private void Awake()
    {
        _05MovingPlatform01.ID = 0;
    }
    private void Start()
    {

        LastPlatform = SpawnMovingPlatform();
        LastPlatform.Speed = 0f;

        // CurrentPlatform=LastPlatform;
    }

    public void OnFire()
    {

        if (CurrentPlatform)
        {
            CurrentPlatform.StopCoroutine(CurrentPlatform.movementCoroutine);  // Debug.Log("Fire 1 Pressed");       #endregion
            CheckHangover();
            LastPlatform = CurrentPlatform;
        }

        CurrentPlatform = SpawnMovingPlatform();

        Camera.main.transform.position += Vector3.up * 0.1f;
        popup.SetTextAndPlayAnimation((++PlatformCount).ToString());
        // Determine spawn position based on look direction



        // Spawn platform with switched look direction

        platformForward = !platformForward; // Switch the bool value for the next spawn

    }

    private _05MovingPlatform01 SpawnMovingPlatform()
    {

        Vector3 spawnDirection = platformForward ? Vector3.forward : Vector3.right;
        Vector3 lastXZ = LastPlatform ?
        new Vector3(LastPlatform.transform.position.x,0,LastPlatform.transform.position.z):
        Vector3.zero;
        spawnPosition = LastPlatform ?
         lastXZ+ spawnDirection * distanceToOrigin + new Vector3(0, height+= 0.1f, 0) :
         Vector3.zero;


        GameObject platformGO = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        Vector3 newScale = LastPlatform ?
         new Vector3(LastPlatform.transform.localScale.x, 0.1f, LastPlatform.transform.localScale.z) :
         new Vector3(1f, 0.1f, 1f);
        platformGO.transform.localScale = newScale;
        _05MovingPlatform01 platformScript = platformGO.GetComponent<_05MovingPlatform01>();
        if (platformScript != null)
        {
            platformScript.Initialize(platformForward);
            platformScript.moveDistance = distanceToOrigin * 2;
            return platformScript;
        }
        return null;

    }


    public void CheckHangover()
    {
        Vector3 curr = CurrentPlatform.transform.position;
        Vector3 last = LastPlatform.transform.position + Vector3.up * 0.1f;
        float distance = Vector3.Distance(curr, last);
        if (distance >= distanceThreshold)
        {
            Debug.LogWarning(distance);
            // gameObject.AddComponent<Rigidbody>();
            //GAME OVER 
            // CurrentPlatform.gameObject.AddComponent<Rigidbody>();
            // SceneMan.Instance?.StartReloadScene();
        }
        else
        {
            if (distance <= applyDistance)
            {
                transform.position = LastPlatform.transform.position + Vector3.up * 0.1f;
            }
            else if (distance > applyDistance)
            {
                // SplitCube();
            }
        }

    }


    private void OnDrawGizmos()
    {
        if (LastPlatform == null || CurrentPlatform == null)
            return;

        // Calculate the distance between the two platforms
        Vector3 curr = CurrentPlatform.transform.position;
        Vector3 last = LastPlatform.transform.position + Vector3.up * 0.1f;
        float distance = Vector3.Distance(curr, last);
        // Set Gizmo color and draw a line between the platforms
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(CurrentPlatform.transform.position, last);

        // Display the distance as a label above the line
        Vector3 labelPosition = last;
        labelPosition.y += 1f;
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.blue;
        Handles.Label(labelPosition, $"Distance: {distance:F2} units", style);
    }
}



