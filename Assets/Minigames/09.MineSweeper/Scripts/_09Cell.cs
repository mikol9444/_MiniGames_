using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
public class _09Cell : MonoBehaviour
{
    public Color defaultColor;
    private Renderer rend;
    TextMeshProUGUI countText;
    Image FlagsImage;
    public bool isMine;
    public bool isRevealed;
    public int surroundingMines;
    public bool flagsOn=false;

    public int X,Y;
    // Constructor to initialize with custom values
    public void Initialize(int i, int j)
    {
        X = i;
        Y = j;
        FlagsImage = GetComponentInChildren<Image>();
        FlagsImage.gameObject.SetActive(false);
    }
    public void SetFlags(){
        FlagsImage.gameObject.SetActive(!flagsOn);
        flagsOn=!flagsOn;
    }
    private void Awake()
    {
        rend = GetComponent<Renderer>();
        defaultColor = rend.material.color;
        countText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void ChangeColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }
    public void ResetColor()
    {
        rend.material.color = defaultColor;
    }
    public void SetCellNeighbors(int count)
    {
        switch (count)
        {
            case 1:
                countText.color = Color.blue;
                break;
            case 2:
                countText.color = Color.green;
                break;
            case 3:
                countText.color = Color.red;
                break;
            case 4:
                countText.color = Color.yellow;
                break;
            case 5:
                countText.color = Color.cyan;
                break;
            case 6:
                countText.color = Color.cyan;
                break;
            case 8:
                countText.color = Color.grey;
                break;
            case 9:
                countText.color = Color.magenta;
                break;
        }
        countText.text = count.ToString();
    }

}
