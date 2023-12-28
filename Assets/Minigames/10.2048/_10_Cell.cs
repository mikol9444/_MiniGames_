using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class _10_Cell : MonoBehaviour
{
    private int currentNumber = 0;
        public TextMeshProUGUI txt;
        public Renderer rend;
    Color startColor = Color.white;
    public int CurrentNumber
    {
        get => currentNumber; set
        {
            currentNumber = value;
            if (value == 0) txt.text = "";
            else
                txt.text = value.ToString();
        }
    }



    public void Initialize(int num)
    {
        startColor = Color.white;
        currentNumber = num;
        txt.text = num.ToString();
        rend = GetComponent<Renderer>();
        // indices.text = "("+i + "," + j+")";
        // gameObject.SetActive(false);
    }
     // Call this function to update the color based on the currentNumber
    public void UpdateCellColor()
    {
        Color cellColor;

        // Map specific numbers to colors
        switch (currentNumber)
        {
            case 2:
                cellColor = new Color(0.5f, 0.0f, 0.0f); // Red
                break;
            case 4:
                cellColor = new Color(1.0f, 0.5f, 0.0f); // Orange
                break;
            case 8:
                cellColor = new Color(1.0f, 1.0f, 0.0f); // Yellow
                break;
            case 16:
                cellColor = new Color(0.0f, 1.0f, 0.0f); // Green
                break;
            case 32:
                cellColor = new Color(0.0f, 0.0f, 1.0f); // Blue
                break;
            case 64:
                cellColor = new Color(0.5f, 0.0f, 1.0f); // Indigo
                break;
            case 128:
                cellColor = new Color(0.8f, 0.0f, 1.0f); // Violet
                break;
            case 256:
                cellColor = new Color(1.0f, 0.0f, 1.0f); // Magenta
                break;
            case 512:
                cellColor = new Color(1.0f, 0.0f, 0.5f); // Pink
                break;
            case 1024:
                cellColor = new Color(0.5f, 0.0f, 0.0f); // Maroon
                break;
            case 2048:
                cellColor = new Color(1.0f, 0.8f, 0.0f); // Bright Yellow
                break;
            case 4096:
                cellColor = new Color(0.9f, 0.1f, 0.0f); // Bright Yellow
                break;
            case 8192:
                cellColor = new Color(1.0f, 0.0f, 0.0f); // Bright Yellow
                break;

            default:
                cellColor = Color.white;
                break;
        }

        // Set the color of the text
        txt.color = cellColor;

        // Set the color of the renderer
        rend.material.color = cellColor;

        // Slightly brighter color for txt
        Color brighterColor = new Color(cellColor.r + 0.2f, cellColor.g + 0.2f, cellColor.b + 0.2f);
        Color darkerColor = new Color(cellColor.r - 0.2f, cellColor.g - 0.2f, cellColor.b - 0.2f);
        txt.color = darkerColor;

        // Slightly brighter color for rend.material
        rend.material.color = brighterColor;
    }

}
