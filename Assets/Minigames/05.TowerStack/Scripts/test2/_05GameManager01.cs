using System.Collections;
using System.Collections.Generic;
using Essentials;
using UnityEngine;
using UnityEditor;
public class _05GameManager01 : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab; // Reference to the platform prefab to instantiate
    public bool switchLookDirection = true; // Global bool value for switching look direction
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

    private void Start()
    {

        GameObject startCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        startCube.GetComponent<Renderer>().material.color = GetRandomColor();
        startCube.transform.localScale = new Vector3(1f, 0.1f, 1f);

        LastPlatform = startCube.AddComponent<_05MovingPlatform01>();
        LastPlatform.Speed = 0;

    }

    public void OnFire()
    {

        if (CurrentPlatform)
        {
            CurrentPlatform.StopAllCoroutines();    // Debug.Log("Fire 1 Pressed");       #endregion
            CheckHangover();
            LastPlatform = CurrentPlatform;
        }
        Vector3 hVector = new Vector3(0, height, 0);
        Vector3 offset = switchLookDirection ? Vector3.right : Vector3.forward;
        offset *= distanceToOrigin;
        spawnPosition = hVector + LastPlatform.transform.position + offset;
        CurrentPlatform = SpawnMovingPlatform(spawnPosition);

        Camera.main.transform.position += Vector3.up * 0.1f;
        popup.SetTextAndPlayAnimation((++PlatformCount).ToString());
        // Determine spawn position based on look direction



        // Spawn platform with switched look direction

        switchLookDirection = !switchLookDirection; // Switch the bool value for the next spawn

    }

    private _05MovingPlatform01 SpawnMovingPlatform(Vector3 position)
    {
        GameObject platformGO = Instantiate(platformPrefab, position, Quaternion.identity);
        Vector3 newScale = new Vector3(LastPlatform.transform.localScale.z, 0.1f, LastPlatform.transform.localScale.x);
        platformGO.transform.localScale = newScale;
        _05MovingPlatform01 platformScript = platformGO.GetComponent<_05MovingPlatform01>();
        if (platformScript != null)
        {
            platformScript.Initialize(switchLookDirection, PlatformCount, distanceToOrigin * 2f);
            return platformScript;
        }
        return null;

    }
    public static Color GetRandomColor()
    {
        float[] colorValues = new float[3];
        for (int i = 0; i < colorValues.Length; i++)
            colorValues[i] = UnityEngine.Random.Range(0, 1f);
        return new Color(colorValues[0], colorValues[1], colorValues[2], 1f);
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
            CurrentPlatform.gameObject.AddComponent<Rigidbody>();
            SceneMan.Instance?.StartReloadScene();
        }
        else
        {
            if (distance <= applyDistance)
            {
                transform.position = LastPlatform.transform.position + Vector3.up * 0.1f;
            }
            else if (distance > applyDistance)
            {
                SplitCube();
            }
        }

    }

    // private void SpawnNewPlatform(_05MovingPlatform01 LastPlatform)
    // {
    //     bool spawnOnZ = FindObjectOfType<_05GameManager01>().switchLookDirection;
    //     float yOffset = 0.1f;
    //     float xPos = spawnOnZ ? 0f : 3f;
    //     float zPos = spawnOnZ ? 3f : 0f;
    //     Quaternion rotation = spawnOnZ ? new Quaternion(0, 1, 0, 2.95042946e-06f) : new Quaternion(0, -0.707105756f, 0, 0.707107902f);

    //     Vector3 pos = new Vector3(xPos, transform.position.y + yOffset, zPos);
    //     // GameObject platform = _05CubeSpawner.Instance.SpawnPlatform(pos, rotation);
    // }
    private void SplitCube()
    {
        // Check if both platforms are valid GameObjects.

        // Determine the cut position based on platform positions and scales.
        Vector3 cutPosition = CalculateCutPosition();
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = cutPosition;
        sphere.transform.localScale = Vector3.one * 0.1f;

        // if (!switchLookDirection)
        // {
        //     float newZSize = LastPlatform.transform.localScale.z - Mathf.Abs(distance);
        //     CurrentPlatform.transform.localScale = new Vector3(CurrentPlatform.transform.localScale.x, 0.1f, newZSize);
        //     distanceThreshold = CurrentPlatform.transform.localScale.z;
        // }
        // // Instantiate a new upper platform.
        // GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // obj.name = "Splitted Cube";

        // // Adjust the scale of the new upper platform.
        // obj.transform.localScale = CalculateNewUpperPlatformScale(cutPosition);

        // // Adjust the scale of the lower platform.
        // LastPlatform.transform.localScale = CalculateNewLowerPlatformScale(cutPosition);

    }
    private Vector3 CalculateCutPosition()
    {
        // if (!switchLookDirection)
        // {
        //     Vector3 dir = CurrentPlatform.movementDirection > 0f ? Vector3.left : Vector3.right;
        //     float splitX = CurrentPlatform.transform.localScale.x-LastPlatform.transform.localScale.x;  
        //     Vector3 pos = new Vector3(
        //         splitX,
        //         CurrentPlatform.transform.position.y,
        //         LastPlatform.transform.position.z) * dir.x;
        //     return pos;
        // }
        // else
        // {
        //     Vector3 dir = CurrentPlatform.movementDirection > 0f ? Vector3.back : Vector3.forward;
        //     dir += new Vector3(0, CurrentPlatform.transform.position.y, 0);
        //     return LastPlatform.transform.position + dir;
        // }

        // else{
        //     return CurrentPlatform.movementDirection >0f? Vector3.back:Vector3.forward;
        // }
        return default;
    }

    private Vector3 CalculateNewUpperPlatformScale(Vector3 cutPosition)
    {
        // Calculate the scale of the new upper platform based on the cut position and original upper platform scale.
        // You'll need to implement your logic here based on your game's requirements.
        // Return the new upper platform scale as a Vector3.
        return Vector3.one; // Placeholder, replace with your logic.
    }

    private Vector3 CalculateNewLowerPlatformScale(Vector3 cutPosition)
    {
        // Calculate the scale of the lower platform based on the cut position and original lower platform scale.
        // You'll need to implement your logic here based on your game's requirements.
        // Return the new lower platform scale as a Vector3.
        return Vector3.one; // Placeholder, replace with your logic.
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



