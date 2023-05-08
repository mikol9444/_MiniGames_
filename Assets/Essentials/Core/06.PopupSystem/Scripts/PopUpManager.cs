using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{

    public Popup popup;
    private void Awake()
    {

        if (!popup) { 
            Debug.LogWarning("No Popups in the Scene");
            Debug.LogWarning("PopUpManager disabled now -.-");
            gameObject.SetActive(false);
        }

    }
    public void TogglePopup(string note)
    {
        popup.OnActivate(note);
    }
}
