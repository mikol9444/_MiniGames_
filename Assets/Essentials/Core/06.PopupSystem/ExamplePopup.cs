using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplePopup : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
            PopUpManager.Instance.ActivateTextPopup("cube fell on me omg");
    }
}
