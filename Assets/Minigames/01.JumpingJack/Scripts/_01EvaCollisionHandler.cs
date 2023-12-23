using UnityEngine;
using System;
using Essentials;
using UnityEngine.Events;
public class _01EvaCollisionHandler : MonoBehaviour
{
    _01EvaMovementController controller;
    ColorBlinker blinker;
    private bool isWon = default;
    public static Action deadAction;
    public UnityEvent onLevelEnd;
    _01EvaMovementController c;
    private void Awake()
    {
        controller = GetComponent<_01EvaMovementController>();
        blinker = GetComponent<ColorBlinker>();


    }
    private void Start()
    {
        c = GetComponent<_01EvaMovementController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            Destroy(other.gameObject);
            AudioManager_Test.Instance.PlaySound("item");
        }
        if (other.CompareTag("Enemy") && controller.isAlive && !isWon)
        {
            blinker.StartBlink(other.gameObject);
            controller.isAlive = false;
            Destroy(controller);
            GetComponent<_01EvaAnimatorController>().SetDeathState();
            SceneMan.Instance.StartReloadScene();
            AudioManager_Test.Instance.PlaySound("dead");
            AudioManager_Test.Instance.StopSound("jetpack");
            this.enabled = false;
            deadAction?.Invoke();
        }
        if (!isWon && other.CompareTag("Goal"))
        {
            isWon = true;
            _01EvaGameMaster.Instance.OnButton1Pressed(); // Should Invoke the Popup
            AudioManager_Test.Instance.PlaySound("win");
            AudioManager_Test.Instance.StopSound("jetpack");
            onLevelEnd?.Invoke();
            Destroy(controller);
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Ground") && c)
        {
            c.firstJumpPerforemed = false; c.secondJumpPerformed = false;
            c.jumpCount = 0;
            if (c.anim) c.anim.setGroundedValue(true);

        }
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.collider.CompareTag("Ground") && c)
        {

            c.isGrounded = true;
            c.jumpCount = 0;
            if (c.anim) c.anim.setGroundedValue(true);

        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            c.isGrounded = false;c.firstJumpPerforemed=true;c.secondJumpPerformed=true;
            if (c.anim) c.anim.setGroundedValue(false);
        }
    }
}
