using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
[CreateAssetMenu(fileName = "InputReader")]
public class InputReader : ScriptableObject, ActionMap.IPlayerActions, ActionMap.IUIActions
{
    #region  Initialize
    //PlayerInput class named as ActionMap implemented as C# script
    [SerializeField] private ActionMap actionMap;

    // Input Events to Invoke in inheritated interface Functions below..
    public event Action<Vector2> MoveEvent;
    public event Action<Vector2> LookEvent;
    public event Action<bool> JumpEvent;
    public event Action<bool> SprintEvent;
    public event Action PauseEvent;
    public event Action<bool> TestEvent;
    public event Action<bool> EnterEvent;


    public void Activate()
    {
        if (actionMap == null) actionMap = new ActionMap();
        Debug.Log("Input Reader activated");
        actionMap.Enable();
        actionMap.Player.SetCallbacks(this);
        actionMap.UI.SetCallbacks(this);
    }
    #endregion

    #region Invoke Actions
    //Vector2 event output from PlayerInput (ActionMap) class 
    public void OnWASD(InputAction.CallbackContext context) => MoveEvent?.Invoke(context.ReadValue<Vector2>());
    public void OnLook(InputAction.CallbackContext context) => LookEvent?.Invoke(context.ReadValue<Vector2>());

    // 5 InputActionPhases possible: Canceled,Disabled,Performed,Started,Waiting
    // on default 3 Invokes: Started->Performed->Canceled
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) JumpEvent?.Invoke(true);
        else if (context.phase == InputActionPhase.Canceled) JumpEvent?.Invoke(false);

    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) SprintEvent.Invoke(true);
        if (context.phase == InputActionPhase.Canceled) SprintEvent.Invoke(false);
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) PauseEvent.Invoke();
    }

    public void OnTest(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) TestEvent.Invoke(true);
        else if (context.phase == InputActionPhase.Canceled) TestEvent.Invoke(false);
    }

    public void OnEnter(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) EnterEvent.Invoke(true);
        if (context.phase == InputActionPhase.Canceled) EnterEvent.Invoke(false);
    }
    #endregion
}
