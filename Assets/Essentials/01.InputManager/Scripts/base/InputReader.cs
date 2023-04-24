using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Essentials
{
    public class InputReader : MonoBehaviour, ActionMap.IPlayerActions, ActionMap.IUIActions
    {
        // PLAYER ACTIONS
        private ActionMap _ActionMap;
        public static event Action<Vector2> _MovementEvent;
        public static event Action<bool> _JumpEvent;
        public static event Action<bool> _SprintEvent;
        public static event Action _InteractEvent;

        //UI ACTIONS
        public static event Action _PauseEvent;
        public static event Action _Button1Event;
        public static event Action _Button2Event;
        public static event Action _Button3Event;

        public void OnMove(InputAction.CallbackContext context) => _MovementEvent?.Invoke(context.ReadValue<Vector2>());
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed) _JumpEvent?.Invoke(true);
            else if (context.phase == InputActionPhase.Canceled) _JumpEvent?.Invoke(false);
        }
        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed) _SprintEvent?.Invoke(true);
            if (context.phase == InputActionPhase.Canceled) _SprintEvent?.Invoke(false);
        }
        public void OnInteract(InputAction.CallbackContext context) => _InteractEvent?.Invoke();
        public void OnButton1(InputAction.CallbackContext context) => _Button1Event?.Invoke();
        public void OnButton2(InputAction.CallbackContext context) => _Button2Event?.Invoke();
        public void OnButton3(InputAction.CallbackContext context) => _Button3Event?.Invoke();
        public void OnPause(InputAction.CallbackContext context) => _PauseEvent?.Invoke();

        private void Awake()
        {
            _ActionMap = new ActionMap();
            _ActionMap.Enable();
            _ActionMap.Player.SetCallbacks(this);
            _ActionMap.UI.SetCallbacks(this);
        }


    }
}