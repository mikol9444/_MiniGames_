using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class UnityAnimationEvent : UnityEvent { };
public class _03SpawnButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private Animation anim;
    [SerializeField] private AnimationClip startAnimation;
    [SerializeField] private AnimationClip endAnimation;
    public UnityAnimationEvent OnAnimationStart;
    public UnityAnimationEvent OnAnimationComplete;
    private bool isPressed;
    private void Awake()
    {
        if(!anim)anim = gameObject.AddComponent<Animation>();
        anim.playAutomatically = false;
        anim.AddClip(startAnimation,"start");
        anim.AddClip(endAnimation, "end");
    }
    public void OnPointerDown(PointerEventData eventData)
    {

        PlayAnimation();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //anim.Play("end");
    }
    public  void PlayAnimation()
    {
        if (!isPressed)
        {
            anim.Play("start");
            isPressed = true;
        }
        else
        {
            anim.Play("end");
            isPressed = false;
        }
    }
    public void AnimationStartHandler(string name)
    {
        Debug.Log($"{name} animation start.");
        OnAnimationStart?.Invoke();
    }
    public void AnimationCompleteHandler(string name)
    {

        gameObject.SetActive(false);
        Debug.Log($"{name} animation complete.");
        OnAnimationComplete?.Invoke();
    }
}
