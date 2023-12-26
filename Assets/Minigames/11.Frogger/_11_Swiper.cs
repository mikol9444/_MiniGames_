using System.Collections;
using System.Collections.Generic;
using Essentials;
using UnityEngine;
using UnityEngine.EventSystems;
public class _11_Swiper : FloatingJoystick
{
        private ExampleInputListener listener;
        public _11_FroggerController controller;
        private float deathZone = 0.8f;
    private void Start()
    {
        listener = FindObjectOfType<ExampleInputListener>();
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        if (listener.movementVector.x > deathZone)
        {
            controller.Jump(Vector2.right);
            Debug.Log("right");
        }
        else if (listener.movementVector.x < -deathZone)
        {
            controller.Jump(Vector2.left);
            Debug.Log("left");
        }
        if (listener.movementVector.y > deathZone)
        {
            controller.Jump(Vector2.up);
            Debug.Log("up");
        }
        else if (listener.movementVector.y < -deathZone)
        {
            controller.Jump(Vector2.down);
            Debug.Log("down");
        }



    }

    public override void QuadrantMarker(Vector2 direction)
    {
        // base.QuadrantMarker(direction);

    }
}
