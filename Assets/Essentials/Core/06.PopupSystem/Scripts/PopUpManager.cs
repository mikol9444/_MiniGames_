using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Instance;
    public Popup[] popups;
    public string popupText = "I SET THIS TEXT OMG";
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        popups = Resources.FindObjectsOfTypeAll<Popup>();
        if (popups.Length==0)
        {
            Debug.LogWarning("No Popups in the Scene");
            Debug.LogWarning("PopUpManager disabled now -.-");
            gameObject.SetActive(false);
        }

    }
    public void ActivateTextPopup(string note = "")
    {
        Popup p = popups[0];

        if (note == "")
        {
            p.SetText(popupText);
        }
        else p.SetText(note);

        if (p.isActiveAndEnabled)
        {
            p.OnDeactivate();
        }
        else p.OnActivate();

    }
}
