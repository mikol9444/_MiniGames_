using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Jack_Target : MonoBehaviour
{
    public static Action OnWinEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WinLevel();
        }
    }
    public static void WinLevel()
    {
        Debug.LogWarning("Level Won!!!!");
        OnWinEvent?.Invoke();
    }
}
