using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using TMPro;
public enum SwipeDirection{RIGHT,LEFT,UP,DOWN}
public class _10_SwipeDetector : FloatingJoystick
{
    private ExampleInputListener listener;
    private _10_GameManager20 manager;
    public Slider coolDownSlider ;
    public TextMeshProUGUI txt;
    Action<SwipeDirection> mergeAction;

    private bool swipeLeft = true;
    public float autoSwipeCoolDown= 0.5f;
    public void ChangeCoolDown(){
        autoSwipeCoolDown = coolDownSlider.value;
        txt.text = autoSwipeCoolDown.ToString("F2");
    }

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
    public void StartAutoSwipe(){
        StartCoroutine(nameof(AutoSwipe));
    }
    public void StopSwiping(){
        StopCoroutine(nameof(AutoSwipe));
    }
        public void StartAutoSwipe2(){
        StartCoroutine(nameof(AutoSwipe2));
    }
    public void StopSwiping2(){
        StopCoroutine(nameof(AutoSwipe2));
    }
    public IEnumerator AutoSwipe(){

        while(true){
            SwipeDirection dir = swipeLeft? SwipeDirection.LEFT : SwipeDirection.DOWN;
            mergeAction(dir);
            swipeLeft = !swipeLeft;
            yield return new WaitForSeconds(autoSwipeCoolDown);
        }

    }
    public IEnumerator AutoSwipe2()
{
    SwipeDirection[] directions = { SwipeDirection.LEFT, SwipeDirection.UP, SwipeDirection.RIGHT, SwipeDirection.DOWN };
    int currentDirectionIndex = 0;

    while (true)
    {
        SwipeDirection dir = directions[currentDirectionIndex];
        mergeAction(dir);

        // Update the direction index for the next iteration
        currentDirectionIndex = (currentDirectionIndex + 1) % directions.Length;

        yield return new WaitForSeconds(autoSwipeCoolDown);
    }
}


}
