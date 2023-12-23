using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Essentials;
public class _01EvaGameMaster : MonoBehaviour
{
    public static _01EvaGameMaster Instance;
    private Transform playerTransform;
    private Transform startPositionTransform;
    private Popup popup;
    public int deathCounter = 0;
    public int currentLevel = 0;
    private void Awake()
    {
        deathCounter = 0;

        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(this);

    }
    private void Start()
    {
        ResetPlayer();
    }
    private void ResetPlayer()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (!playerTransform) Debug.LogWarning("No Player Detected");
        startPositionTransform = GameObject.FindGameObjectWithTag("Start")?.transform;
        if (!startPositionTransform) Debug.LogWarning("No start Flag Detected");
        popup = FindObjectOfType<Popup>();
        if (!popup) Debug.LogWarning("No Popup Detected");

        if (playerTransform && startPositionTransform)
            playerTransform.position = new Vector3(startPositionTransform.position.x, startPositionTransform.position.y, 0f);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        _01EvaCollisionHandler.deadAction += _DeathScore;
        if (InputManager.Instance)
        {
            InputManager.Instance._Button1Event += OnButton1Pressed;
        }


    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        _01EvaCollisionHandler.deadAction -= _DeathScore;
        InputManager.Instance._Button1Event -= OnButton1Pressed;
    }
    public void OnButton1Pressed()
    {
        popup.OnActivate("WIN");
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetPlayer();
        if (scene.name == "01_level1")
        {
            deathCounter = 0;
        }
    }

    private void _DeathScore() => ++deathCounter;

}
