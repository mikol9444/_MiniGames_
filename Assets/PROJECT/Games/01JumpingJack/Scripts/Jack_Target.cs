using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack_Target : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WinLevel();
        }
    }
    public void WinLevel()
    {
        Debug.LogWarning("Level Won!!!!");
    }
}
