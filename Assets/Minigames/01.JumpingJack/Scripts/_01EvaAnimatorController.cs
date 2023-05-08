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
    private void OnEnable()
    {
        InputManager._JumpEvent += OnJumpPerformed;
        InputManager._CrouchEvent += OnCrouchPerformed;
        InputManager._MovementEvent += OnMovementActive;
    }
    private void OnDisable()
    {
        InputManager._JumpEvent -= OnJumpPerformed;
        InputManager._CrouchEvent -= OnCrouchPerformed;
        InputManager._MovementEvent -= OnMovementActive;
    }
    private void OnJumpPerformed(bool state) {
        anim.SetBool("jump", state);

    }
    private void OnCrouchPerformed(bool state) {
        anim.SetBool("crouch", state);

    }
    private void OnMovementActive(Vector2 dir) {

        anim.SetFloat("Blend", dir.magnitude);
    }
    public void SetDeathState() => anim.SetBool("dead", true);
}
