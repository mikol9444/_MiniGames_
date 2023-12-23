using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class _06RaycastExample : MonoBehaviour
{
    public Button playButton;
private RaycastHit hit;

    void Update()
    {
        // Check for mouse input or touch input
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            PerformRaycast(ray);
        }
        else if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
            PerformRaycast(ray);
        }
    }

    void PerformRaycast(Ray ray)
    {
        // Check for collision with an object
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

            if (hit.collider.CompareTag("Player"))
            {
                // The ray hit an object with the "Player" tag
                Debug.Log("HIT!!!!!!!!!!!!!!!!!!!!!!");
                playButton.onClick.Invoke();
                // You can perform additional actions or logic here
            }
            else{
                  Debug.Log("NO ENEMY HIT");
            }
        }
        else{
             Debug.Log("NO COLLIDER DETECTED");
        }
    }
}
