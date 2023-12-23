using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _01GroundFeedback : MonoBehaviour
{
    public Animation anim;
    public AnimationClip onClip;
    public AnimationClip offClip;
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            Debug.Log("PLAYER ENTER");
            anim.clip = onClip;
            anim.Play();


        }
    }
    public void PlayOffAnimation(){
            anim.clip = offClip;
            anim.Play();
    }
}
