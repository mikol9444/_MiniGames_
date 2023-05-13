using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class _04ScreenTapper : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int tapCount = 0;
    public Animation anim;
    public TextMeshProUGUI tapCountUI;
    public TextMeshProUGUI timerUI;
    public Slider timeSlider;
    public float timeCount = 0f;
    bool isPressed = false;
    bool firstTimePressed = false;
    public Popup popup;
    private void OnEnable()
    {
        timeCount = timeSlider.value;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!firstTimePressed)
        {
            tapCount = 0;
            firstTimePressed = true;
            timeSlider.interactable = false;
            StartCoroutine(nameof(TimerRoutine));
        }
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    public void StartAnim()
    {
        if (isPressed) return;

        anim.Play();

        tapCountUI.text = $"Taps {++tapCount}";

    }
    public void onSliderChange() => timerUI.text = timeSlider.value.ToString("0.00");
    public IEnumerator TimerRoutine()
    {
        float startTime = timeSlider.value;
        while (timeSlider.value > 0)
        {
            timeSlider.value -= Time.deltaTime;
            yield return null;
        }
        timeSlider.interactable = true;
        firstTimePressed = false;
        timeSlider.value = startTime;
        popup.OnActivate($"you tapped {tapCount} times in : {startTime.ToString("0.00")} seconds");

        
    }
}
