using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials;
using UnityEngine.EventSystems;
using System;
    public enum SwipeDirection{RIGHT,LEFT,UP,DOWN}
public class _10_SwipeDetector : FloatingJoystick
{
    private ExampleInputListener listener;
    private _10_GameManager20 manager;
    Action<SwipeDirection> mergeAction;

    private void Start()
    {

        listener = FindObjectOfType<ExampleInputListener>();
        manager = FindObjectOfType<_10_GameManager20>();
        mergeAction =  manager.MoveTheCells;
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        if (listener.movementVector.x > 0.8f) mergeAction(SwipeDirection.RIGHT);
        else if (listener.movementVector.x < -0.8f) mergeAction(SwipeDirection.LEFT);

        if (listener.movementVector.y > 0.8f) mergeAction(SwipeDirection.UP);
        else if (listener.movementVector.y < -0.8f) mergeAction(SwipeDirection.DOWN);



    }
    public override void QuadrantMarker(Vector2 direction){ } //Do not need this functionality from the parent class

}
