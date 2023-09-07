using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class _01LevelUNlocker : MonoBehaviour
{
    public GameObject parentObject;
    private Button[] buttons;
    private Animation[] anims;

    private void Awake()
    {
        int childCount = parentObject.transform.childCount;
        buttons = new Button[childCount];
        anims = new Animation[childCount];
        for (int i = 0; i < childCount; i++)
        {
            Transform tr = parentObject.transform.GetChild(i);
            buttons[i]=tr.GetComponent<Button>();
            anims[i]=tr.GetComponent<Animation>();
        }

    }
    private void Start()
    {
        int levelProgress = _01EasySaveData.Instance.Load();
        for (int i = 0; i <= levelProgress; i++)
        {
            buttons[i].interactable = true;
            buttons[i].image.color = Color.white;
            anims[i].enabled = true;
        }
    }
    public void ResetLevels()
    {
        int childCount = parentObject.transform.childCount;
        for (int i = 1; i < childCount; i++)
        {
            buttons[i].interactable = false;
            anims[i].enabled = false;
        }
    }
}
