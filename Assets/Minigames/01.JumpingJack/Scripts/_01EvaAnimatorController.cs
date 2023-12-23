using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials;
public class _01EvaAnimatorController : MonoBehaviour
{


    private Animator anim;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

    }
    private void Start()
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
    public void SetBlendValue(float value)
    {
        anim.SetFloat("jumpBlend", value);
    }
    public void setGroundedValue(bool value)
    {
        anim.SetBool("isGrounded", value);
    }
    public void OnJumpPerformed(bool state)
    {
        anim.SetBool("jump", state);
    }
    private void OnCrouchPerformed(bool state)
    {
        anim.SetBool("crouch", state);

    }
    private void OnMovementActive(Vector2 dir)
    {

        anim.SetFloat("Blend", dir.magnitude);
    }
    public void SetDeathState() => anim.SetBool("dead", true);
}
