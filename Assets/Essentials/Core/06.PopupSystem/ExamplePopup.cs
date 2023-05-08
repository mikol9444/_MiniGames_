using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplePopup : MonoBehaviour
{
    PopUpManager man;
    private void Awake()
    {
        man = FindObjectOfType<PopUpManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        man.TogglePopup("cube fell on me omg");
    }
}
