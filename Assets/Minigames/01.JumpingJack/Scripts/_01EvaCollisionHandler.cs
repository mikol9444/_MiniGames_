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
    private void Awake()
    {
        controller = GetComponent<_01EvaMovementController>();
        blinker = GetComponent<ColorBlinker>();

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
        if (!isWon &&other.CompareTag("Goal"))
        {
            isWon = true;
            _01EvaGameMaster.Instance.OnButton1Pressed(); // Should Invoke the Popup
            AudioManager_Test.Instance.PlaySound("win");
            AudioManager_Test.Instance.StopSound("jetpack");
            onLevelEnd?.Invoke();
            Destroy(controller);
        }
    }
}
