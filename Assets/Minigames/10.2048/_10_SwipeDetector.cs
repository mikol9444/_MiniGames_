using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials;
using UnityEngine.EventSystems;
public class _10_SwipeDetector : FloatingJoystick
{
    private ExampleInputListener listener;
    private _10_GameManager manager;

    private void Start()
    {
        listener = FindObjectOfType<ExampleInputListener>();
        manager = FindObjectOfType<_10_GameManager>();
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (listener.movementVector.x > 0.8f)
        {
            Debug.Log("right");
            manager.MoveCells(MoveDirection.Right);
        }
        else if (listener.movementVector.x < -0.8f)
        {
            Debug.Log("left");
            manager.MoveCells(MoveDirection.Left);
        }
        if (listener.movementVector.y > 0.8f)
        {
            Debug.Log("up");
            manager.MoveCells(MoveDirection.Up);
        }
        else if (listener.movementVector.y < -0.8f)
        {
            Debug.Log("down");
            manager.MoveCells(MoveDirection.Right);
        }

    }
    public override void QuadrantMarker(Vector2 direction)
    {
        return;
        // base.QuadrantMarker(direction);
    }
}
