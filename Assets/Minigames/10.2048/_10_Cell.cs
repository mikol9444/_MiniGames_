using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class _10_Cell : MonoBehaviour
{
    private int currentNumber=0;
    public int CurrentNumber {get=>currentNumber; set {
        currentNumber=value;
        if(value == 0)txt.text ="";
        else
        txt.text=value.ToString();}
        }
    public TextMeshProUGUI txt;


    public void Initialize(int num){
        currentNumber = num;
        txt.text = num.ToString();
        // indices.text = "("+i + "," + j+")";
        // gameObject.SetActive(false);
    }
}
