using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster01 : MonoBehaviour
{
    private void Start()
    {
        Jack_Target.OnWinEvent += WinThisLevel;
    }
    private void OnDisable()
    {
        Jack_Target.OnWinEvent -= WinThisLevel;
    }
    public void WinThisLevel()
    {
        FindObjectOfType<JackController>().rb.velocity = Vector3.zero;
        FindObjectOfType<JackController>().enabled = false;
    }
}
