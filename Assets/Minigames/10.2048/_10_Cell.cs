using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class _10_Cell : MonoBehaviour
{
    public int currentNumber=0;
    public TextMeshProUGUI txt;

    private void Start() {
        ChangeNumber(2);
    }
    public void ChangeNumber(int num){
        txt.text = num.ToString();
    }
}
