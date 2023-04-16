using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack_Detector : MonoBehaviour
{
    Renderer rend;
    Color defaultColor;
    Color PressedColor = Color.cyan;
    private int index;
    public int Index { get => index; set => index = value; }
    private CubeSpawner_test cubeSpawner;

    private void Start()
    {
        rend = GetComponentInParent<Renderer>();
        defaultColor = rend.material.color;
        cubeSpawner = FindObjectOfType<CubeSpawner_test>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            TriggerON();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            TriggerOFF();
        }
    }
    public void TriggerON()
    {
        rend.material.color = PressedColor;
        cubeSpawner.SpawnCube(Index);
    }
    public void TriggerOFF()
    {

        rend.material.color = defaultColor;

    }
}
