using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionAnimator : MonoBehaviour
{
    private static Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        StartScene();
    }
    public static void EndScene() => anim.SetTrigger("end");
    public static void StartScene() => anim.SetTrigger("start");
}
