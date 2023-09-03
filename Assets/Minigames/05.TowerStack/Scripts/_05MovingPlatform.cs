using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class _05MovingPlatform : MonoBehaviour
{
    public static _05MovingPlatform LastPlatform { get; set; }
    public static _05MovingPlatform CurrentPlatform { get; set; }
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float direction = 1f;
    private void Start()
    {
        if (LastPlatform == null)
        {
            LastPlatform = GameObject.Find("Start").GetComponent<_05MovingPlatform>();
            CurrentPlatform = GameObject.Find("1_Platform").GetComponent<_05MovingPlatform>();
        }

        GetComponent<Renderer>().material.color = GetRandomColor();
        if (this == LastPlatform)
        {
            Debug.Log($"LastPlatform is called {LastPlatform.name}");
            Debug.Log($"CurrentPlatform is called {CurrentPlatform.name}");
        }
    }
    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), 1f);
    }
    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed * direction;
    }
    public void Stop()
    {
        moveSpeed = 0;
        CheckHangover();

        // direction = hangover > 0 ? 1f : -1f;
        // Debug.Log(hangover);
    }
    private void CheckHangover()
    {
        direction = CurrentPlatform.transform.position.x > 0 ? 1f : -1f;
        if (FindObjectOfType<_05GameManager>().spawnOnZ)
        {
            float value = CurrentPlatform.transform.position.x - LastPlatform.transform.position.x;
            if (Mathf.Abs(value) < 0.025f)
            {
                CurrentPlatform.transform.position = new Vector3(
                    LastPlatform.transform.position.x,
                    CurrentPlatform.transform.position.y,
                    LastPlatform.transform.position.z);
                SpawnNewPlatform();
                return;
            }
            if (Mathf.Abs(value) >= LastPlatform.transform.localScale.z)
            {
                CurrentPlatform.gameObject.AddComponent<Rigidbody>();
                StartCoroutine(nameof(ReloadLevel));
                return;
            }
        }
        else
        {
            float value = CurrentPlatform.transform.position.z - LastPlatform.transform.position.z;
            if (Mathf.Abs(value) < 0.025f)
            {
                CurrentPlatform.transform.position = new Vector3(
                    LastPlatform.transform.position.x,
                    CurrentPlatform.transform.position.y,
                    LastPlatform.transform.position.z);
                SpawnNewPlatform();
                return;
            }
            Debug.LogWarning("VALUE: " + value + "Local scale last Platform: " + LastPlatform.transform.localScale.x);
            if (Mathf.Abs(value) >= LastPlatform.transform.localScale.x)
            {

                CurrentPlatform.gameObject.AddComponent<Rigidbody>();
                StartCoroutine(nameof(ReloadLevel));
                return;
            }
        }

        SplitCube();
        SpawnNewPlatform();
    }
    private void SpawnNewPlatform()
    {
        bool spawnOnZ = FindObjectOfType<_05GameManager>().spawnOnZ;
        Vector3 pos = spawnOnZ ?
            new Vector3(0, _05MovingPlatform.CurrentPlatform.transform.position.y + 0.1f, 3f) :
            new Vector3(3f, _05MovingPlatform.CurrentPlatform.transform.position.y + 0.1f, 0f);
        Quaternion rot = spawnOnZ ? new Quaternion(0, 1, 0, 2.95042946e-06f) : new Quaternion(0, -0.707105756f, 0, 0.707107902f);
        GameObject platform = _05CubeSpawner.Instance.SpawnPlatform(pos, rot);
    }
    private void SplitCube()
    {
        bool spawnOnZ = FindObjectOfType<_05GameManager>().spawnOnZ;
        if (spawnOnZ)
        {
            float hangover = CurrentPlatform.transform.position.x - LastPlatform.transform.position.x;
            float newZSize = LastPlatform.transform.localScale.z - Mathf.Abs(hangover);
            float fallingBlockSize = transform.localScale.z - newZSize;

            float newXPosition = LastPlatform.transform.position.x + (hangover / 2);
            CurrentPlatform.transform.localScale = new Vector3(CurrentPlatform.transform.localScale.x, CurrentPlatform.transform.localScale.y, Mathf.Abs(newZSize));
            CurrentPlatform.transform.position = new Vector3(newXPosition, CurrentPlatform.transform.position.y, CurrentPlatform.transform.position.z);

            float cubeEdge = CurrentPlatform.transform.position.x + (newZSize / 2f * direction);
            float fallingBlockXPosition = cubeEdge + fallingBlockSize / 2f * direction;

            // var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            // sphere.transform.position = new Vector3(cubeEdge, transform.position.y, transform.position.z);
            // sphere.transform.localScale = Vector3.one * 0.25f;
            SpawnDropCube(fallingBlockXPosition, fallingBlockSize);
        }
        else
        {
            float hangover = CurrentPlatform.transform.position.z - LastPlatform.transform.position.z;
            float newZSize = LastPlatform.transform.localScale.x - Mathf.Abs(hangover);
            float fallingBlockSize = transform.localScale.z - newZSize;

            float newZPosition = LastPlatform.transform.position.z + (hangover / 2);
            CurrentPlatform.transform.localScale = new Vector3(CurrentPlatform.transform.localScale.x, CurrentPlatform.transform.localScale.y, newZSize);
            CurrentPlatform.transform.position = new Vector3(CurrentPlatform.transform.position.x, CurrentPlatform.transform.position.y, newZPosition);

            float cubeEdge = CurrentPlatform.transform.position.z + (newZSize / 2f * direction);
            float fallingBlockZPosition = cubeEdge + fallingBlockSize / 2f * direction;

            // var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            // sphere.transform.position = new Vector3(transform.position.x, transform.position.y, cubeEdge);
            // sphere.transform.localScale = Vector3.one * 0.25f;
            SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
        }

    }
    private void SpawnDropCube(float fallingBlockZPosition, float fallingBlockSize)
    {
        bool spawnOnZ = FindObjectOfType<_05GameManager>().spawnOnZ;
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (spawnOnZ)
        {
            
            cube.transform.localScale = new Vector3(fallingBlockSize, 0.1f, LastPlatform.transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockZPosition, CurrentPlatform.transform.position.y, CurrentPlatform.transform.position.z);
            cube.AddComponent<Rigidbody>();
            cube.GetComponent<Collider>().isTrigger = true;
            cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
            Destroy(cube.gameObject, 2.5f);
        }
        else
        {
            cube.transform.localScale = new Vector3(LastPlatform.transform.localScale.z, 0.1f, fallingBlockSize);
            cube.transform.position = new Vector3(CurrentPlatform.transform.position.x, CurrentPlatform.transform.position.y, fallingBlockZPosition);
            cube.AddComponent<Rigidbody>();
            cube.GetComponent<Collider>().isTrigger = true;
            cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
            Destroy(cube.gameObject, 2.5f);
        }

    }
    private IEnumerator ReloadLevel()
    {
        enabled = false;
        FindObjectOfType<_05GameManager>().enabled = false;
        yield return new WaitForSeconds(1f);
        SceneMan.Instance.StartReloadScene();
    }
}
