using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _05MovingPlatform : MonoBehaviour
{
    public static _05MovingPlatform LastPlatform { get; set; }
    public static _05MovingPlatform CurrentPlatform { get; set; }
    [SerializeField] private float moveSpeed = 1f;
    private void OnEnable()
    {
        if (LastPlatform == null)
        {
            LastPlatform = GameObject.Find("Start").GetComponent<_05MovingPlatform>();
            // if (LastPlatform == null)
            // {
            //     Debug.Log("HMMM");
            // }
        }
        if (CurrentPlatform == null)
        {
            CurrentPlatform = GameObject.Find("Start_01").GetComponent<_05MovingPlatform>();
        }
        GetComponent<Renderer>().material.color = GetRandomColor();
        if (this == LastPlatform)
        {
            Debug.Log($"LastPlatform is called {LastPlatform.name}");
            Debug.Log($"CurrentPlatform is called {CurrentPlatform.name}");
        }
    }
    private void OnDisable()
    {
        Destroy(this);
    }
    private Color GetRandomColor()
    {
        return new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1f);
    }
    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
    public void Stop()
    {
        moveSpeed = 0;
        float hangover = CurrentPlatform.transform.position.x - LastPlatform.transform.position.x;

        if (Mathf.Abs(hangover) >= LastPlatform.transform.localScale.z)
        {
            LastPlatform = null;
            CurrentPlatform = null;
            FindObjectOfType<_05GameManager>().DisableEvents();
            SceneMan.Instance.StartReloadScene();

        }
        float direction = hangover > 0 ? 1f : -1f;
        Debug.Log(hangover);
        // SplitCubeOnZ(hangover, direction);
    }
}
