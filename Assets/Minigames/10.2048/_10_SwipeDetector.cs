using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials;
using UnityEngine.EventSystems;
public class _10_SwipeDetector : FloatingJoystick
{
    private ExampleInputListener listener;
    private _10_GameManager20 manager;

    private void Start()
    {
        listener = FindObjectOfType<ExampleInputListener>();
        manager = FindObjectOfType<_10_GameManager20>();
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        if (listener.movementVector.x > 0.8f)
        {
            Debug.Log("right");
            manager.StartCoroutine(manager.MergeCellsRight());
        }
        else if (listener.movementVector.x < -0.8f)
        {
            Debug.Log("left");
            manager.StartCoroutine(manager.MergeCellsLeft());
        }
        if (listener.movementVector.y > 0.8f)
        {
            Debug.Log("up");
            manager.StartCoroutine(manager.MergeCellsUp());
        }
        else if (listener.movementVector.y < -0.8f)
        {
            Debug.Log("down");
            manager.StartCoroutine(manager.MergeCellsDown());
        }

        else{
            Debug.LogWarning("ADGLMSDÖLGMSDAÖLFGSDDLÖFG");
        }


    }
    public override void QuadrantMarker(Vector2 direction)
    {
        return;
        // base.QuadrantMarker(direction);
    }
}
