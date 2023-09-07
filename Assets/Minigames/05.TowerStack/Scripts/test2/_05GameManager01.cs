using System.Collections;
using System.Collections.Generic;
using Essentials;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.EventSystems;
public class _05GameManager01 : MonoBehaviour, IPointerDownHandler
{
    [Header("Setup Fields")]
    [SerializeField] private GameObject platformPrefab; // Reference to the platform prefab to instantiate
    [SerializeField] private Popup scorePopupup;
    [SerializeField] private Popup gameoverPopup;

    [Header("Settings")]
    [SerializeField] private float height;
    [SerializeField] private float distanceToOrigin = 3f;
    [SerializeField] private float applyDistance = 0.25f;
    [SerializeField] private float platformSpeed = 0.5f;
    public Gradient gradient;
    private float currentGradientValue = 0.0f;



    //Debug Section
    private bool SpawnPlatformInfront; // Global bool value for switching look direction
    [SerializeField] private float distanceThreshold = 0.5f;
    [SerializeField] private _05MovingPlatform01 CurrentPlatform; // Reference to the current platform Gameobject to instantiate
    [SerializeField] private _05MovingPlatform01 LastPlatform;// Reference to the last platform Gameobject to instantiate
    [SerializeField] private Vector3 spawnPosition = Vector3.zero; // Initial spawn position
    [SerializeField] private int PlatformCount = 0;
    [SerializeField] private float direction = 1f;

    private void Awake()
    {
        _05MovingPlatform01.ID = 0;
    }
    private void Start()
    {

        LastPlatform = SpawnMovingPlatform();
        LastPlatform.Speed = 0f;
        CameraFollow cam = FindObjectOfType<CameraFollow>();
        cam.playerTransform = LastPlatform.transform;
        ;
        // CurrentPlatform=LastPlatform;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnFire();
    }
    public Color gradientValue()
    {
        return gradient.Evaluate(currentGradientValue);
    }
    public void OnFire()
    {
        height += 0.1f;
        if (CurrentPlatform)
        {
            CurrentPlatform.StopCoroutine(CurrentPlatform.movementCoroutine);  // Debug.Log("Fire 1 Pressed");       #endregion
            if (CheckHangover())
            {
                return;
            }
            LastPlatform = CurrentPlatform;

        }

        CurrentPlatform = SpawnMovingPlatform();

        // Camera.main.transform.position += Vector3.up * 0.1f;
        // FindObjectOfType<CameraFollow>().playerTransform = LastPlatform.transform;
        CameraFollow cam = FindObjectOfType<CameraFollow>();
        cam.cameraOffset += new Vector3(0,0.1f,0);
        scorePopupup.SetTextAndPlayAnimation((++PlatformCount).ToString());
        // Determine spawn position based on look direction



        // Spawn platform with switched look direction

        SpawnPlatformInfront = !SpawnPlatformInfront; // Switch the bool value for the next spawn

    }

    private _05MovingPlatform01 SpawnMovingPlatform()
    {
        if (currentGradientValue >= 1f) currentGradientValue = 0f;
        currentGradientValue += 0.07f;
        Vector3 spawnDirection = SpawnPlatformInfront ? Vector3.forward : Vector3.right;
        Vector3 lastXZ = LastPlatform ?
        new Vector3(LastPlatform.transform.position.x, 0, LastPlatform.transform.position.z) :
        Vector3.zero;
        spawnPosition = LastPlatform ?
         lastXZ + spawnDirection * distanceToOrigin + new Vector3(0, height, 0) :
         Vector3.zero;


        GameObject platformGO = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        Vector3 newScale = LastPlatform ?
         new Vector3(LastPlatform.transform.localScale.x, 0.1f, LastPlatform.transform.localScale.z) :
         new Vector3(1f, 0.1f, 1f);
        platformGO.transform.localScale = newScale;
        _05MovingPlatform01 platformScript = platformGO.GetComponent<_05MovingPlatform01>();
        if (platformScript != null)
        {
            platformScript.Initialize(SpawnPlatformInfront, platformSpeed);
            platformScript.moveDistance = distanceToOrigin * 2;
            return platformScript;
        }

        return null;

    }


    public bool CheckHangover()
    {

        Vector3 curr = CurrentPlatform.transform.position;
        Vector3 last = LastPlatform.transform.position + Vector3.up * 0.1f;
        float distance = Vector3.Distance(curr, last);
        Transform t = LastPlatform.transform;
        distanceThreshold = SpawnPlatformInfront ? t.localScale.x : t.localScale.z;
        if (distance < distanceThreshold)
        {
            if (distance <= applyDistance)
            {
                CurrentPlatform.transform.position = LastPlatform.transform.position + Vector3.up * 0.1f;
                AudioManager_Test.Instance.PlaySound("applySound");

            }
            else
            {
                Debug.Log($"Splitting! Distance: {distance} distanceThreshold: {distanceThreshold} applyDistance: {applyDistance}");
                SplitCube(distance);
                AudioManager_Test.Instance.PlaySound("splitSound");
            }

        }
        else
        {
            StartCoroutine(GameoverRoutine(2.5f));
            return true;
        }
        return false;


    }
    private void SplitCube(float distance)
    {
        Transform t = CurrentPlatform.transform;
        GameObject restCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        restCube.GetComponent<Renderer>().material = LastPlatform.GetComponent<Renderer>().material;
        if (SpawnPlatformInfront)
        {

            restCube.transform.localScale = new Vector3(distance, t.localScale.y, t.localScale.z);
            restCube.GetComponent<Renderer>().material.color = CurrentPlatform.GetComponent<Renderer>().material.color;


            t.localScale -= new Vector3(distance, 0, 0);
            t.position = t.position.x > LastPlatform.transform.position.x ?
            t.position - new Vector3(distance * 0.5f, 0, 0) :
            t.position + new Vector3(distance * 0.5f, 0, 0);

            float spawnOnXValue = restCube.transform.localScale.x / 2 + t.localScale.x / 2;
            Debug.Log($"SPLITTING ON X move x: {spawnOnXValue}");
            restCube.transform.position = t.position.x > LastPlatform.transform.position.x ?
            t.position + new Vector3(spawnOnXValue, 0, 0) :
            t.position - new Vector3(spawnOnXValue, 0, 0);
        }
        else
        {
            restCube.transform.localScale = new Vector3(t.localScale.x, t.localScale.y, distance);
            restCube.GetComponent<Renderer>().material.color = CurrentPlatform.GetComponent<Renderer>().material.color;
            //Reset Scale and position of the Platform itself
            t.localScale -= new Vector3(0, 0, distance);
            t.position = t.position.z > LastPlatform.transform.position.z ?
            t.position - new Vector3(0, 0, distance * 0.5f) :
            t.position + new Vector3(0, 0, distance * 0.5f);

            float spawnOnZValue = restCube.transform.localScale.z / 2 + t.localScale.z / 2;
            Debug.Log($"SPLITTING ON X move z: {spawnOnZValue}");
            restCube.transform.position = t.position.z > LastPlatform.transform.position.z ?
            t.position + new Vector3(0, 0, spawnOnZValue) :
            t.position - new Vector3(0, 0, spawnOnZValue);

        }
        restCube.AddComponent<Rigidbody>();
        Destroy(restCube, 3f);

    }
    private IEnumerator GameoverRoutine(float time)
    {
        //GAME OVER 
        enabled = false;
        CurrentPlatform.gameObject.AddComponent<Rigidbody>();
        yield return new WaitForSeconds(time);
        gameoverPopup.OnActivate("Game over");


    }
#if UNITY_EDITOR
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
#endif
}



