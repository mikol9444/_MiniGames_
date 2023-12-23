using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Popup : MonoBehaviour
{
    public Animation anim;
    public AnimationClip popinClip;
    public AnimationClip popupClip;
    public TextMeshProUGUI txt;
    private string welcomeText = "Hello World";
    private void Awake()
    {
        anim = GetComponent<Animation>();
    }
    private void OnEnable()
    {
        txt.text = welcomeText;
    }
    public void SetActiveFalse() => gameObject.SetActive(false);
    public void OnActivate(string context = default)
    {
        gameObject.SetActive(true);
        txt.text = context;
        anim.clip = popupClip;
        anim.Play();
    }
    public void OnDeactivate()
    {
        anim.clip = popinClip;
        anim.Play();

    }
    public void SetText(string note) => welcomeText = note;
    public void SetTextAndPlayAnimation(string note)
    {
        txt.text = note;
        anim.Play();
    }
}
