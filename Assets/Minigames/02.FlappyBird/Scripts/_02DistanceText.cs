using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class _02DistanceText : MonoBehaviour
{

    private TextMeshProUGUI txt;
    private Transform playerTransform;

    private void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    void Update()
    {
        txt.text = "Distance: " + playerTransform.position.x.ToString("0") + " m";
    }
}
