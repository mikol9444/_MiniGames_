using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Initialization/Subscribtion 
    public static InputManager Instance;
    public InputReader _inputReader;
    [Header("Input Values")]
    public Vector2 move = new Vector2();
    public Vector2 look = new Vector2();
    public bool jump = false;
    public bool sprint = false;
    public bool gamePaused = false;
    public bool test = false;
    public bool MoveXOnly = false;
    public bool enter = false;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {

        _inputReader.Activate();
        _inputReader.MoveEvent += OnMove;
        _inputReader.LookEvent += OnLook;
        _inputReader.JumpEvent += OnJump;
        _inputReader.SprintEvent += OnSprint;
        _inputReader.PauseEvent += OnPause;
        // _inputReader.TestEvent += OnTest;
        _inputReader.EnterEvent += OnEnter;
    }
    private void OnApplicationQuit()
    {
        _inputReader.MoveEvent -= OnMove;
        _inputReader.LookEvent -= OnLook;
        _inputReader.JumpEvent -= OnJump;
        _inputReader.SprintEvent -= OnSprint;
        _inputReader.PauseEvent -= OnPause;
        // _inputReader.TestEvent -= OnTest;
        _inputReader.EnterEvent -= OnEnter;
    }
    #endregion

    #region Listeners
    public void OnMove(Vector2 dir) => move = MoveXOnly ? new Vector2(dir.x, 0) : dir;
    public void OnLook(Vector2 dir) => look = dir;
    public void OnJump(bool state) => jump = state;
    public void OnSprint(bool state) => sprint = state;
    public void OnPause() => gamePaused = !gamePaused;
    // public void OnTest(bool state) => test = state;
    public void OnEnter(bool state) => enter = state;

    public void Deactivate()
    {
        foreach (var item in GetComponentsInChildren<Transform>())
        {
            item.gameObject.SetActive(false);
        }
    }
    #endregion
}
