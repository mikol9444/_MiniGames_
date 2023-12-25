using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Essentials
{
    public class InputManager : MonoBehaviour, ActionMap.IPlayerActions, ActionMap.IUIActions
    {
        // PLAYER ACTIONS
        private ActionMap _ActionMap;
        public static InputManager Instance;
        public event Action<Vector2> _MovementEvent;
        public event Action<bool> _JumpEvent;
        public event Action<bool> _SprintEvent;
        public event Action _InteractEvent;
        public event Action<bool> _CrouchEvent;
        public event Action _Fire1Event;


        //UI ACTIONS
        public event Action _PauseEvent;
        public event Action _Button1Event;
        public event Action _Button2Event;
        public event Action _Button3Event;
        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            Instance = this;
            if (_ActionMap == null)
            {
                _ActionMap = new ActionMap();
                _ActionMap.Enable();
                _ActionMap.Player.SetCallbacks(this);
                _ActionMap.UI.SetCallbacks(this);
            }

        }

        public void ClearJumpEvent()
        {
            if (_JumpEvent != null)
            {
                Delegate[] delegates = _JumpEvent.GetInvocationList();
                foreach (Delegate del in delegates)
                {
                    _JumpEvent -= (Action<bool>)del;
                }
            }
        }
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

        public void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _CrouchEvent?.Invoke(true);
            }
            else if (context.canceled)
            {
                _CrouchEvent?.Invoke(false);
            }
        }
        public void OnFire1(InputAction.CallbackContext context)
        {

            if (context.performed)
            {
                _Fire1Event?.Invoke();//
            }
        }
    }
}