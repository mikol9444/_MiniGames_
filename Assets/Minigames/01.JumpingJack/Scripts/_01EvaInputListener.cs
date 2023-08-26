using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials;
public class _01EvaInputListener : MonoBehaviour
{
    private bool isJumpingPressed;
    public bool IsJumpingPressed { get => isJumpingPressed; }

    private bool isCrouchingPressed;
    public bool IsCrouchingPressed { get => isCrouchingPressed; }

    private Vector3 moveDirection;
    public Vector3 MoveDirection { get => moveDirection; }


    private void OnEnable()
    {
        InputManager.Instance._JumpEvent += OnJumpPerformed;
        InputManager.Instance._CrouchEvent += OnCrouchPerformed;
        InputManager.Instance._MovementEvent += OnMovementActive;
    }
    private void OnDisable()
    {

        InputManager.Instance._JumpEvent -= OnJumpPerformed;
        InputManager.Instance._CrouchEvent -= OnCrouchPerformed;
        InputManager.Instance._MovementEvent -= OnMovementActive;
    }
    private void OnJumpPerformed(bool state)
    {
        isJumpingPressed = state;
        Debug.Log("Jumping!");
    }
    private void OnCrouchPerformed(bool state) => isCrouchingPressed = state;
    private void OnMovementActive(Vector2 dir) => moveDirection = dir;


}