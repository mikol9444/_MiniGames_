using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class _09RaycastingCells : MonoBehaviour
{
    public _09Cell lastCell;
    public _09Cell currentCell;
    _09GameLogic gameLogic;
    private void Start()
    {
        gameLogic = FindObjectOfType<_09GameLogic>();
    }
    private RaycastHit hit;



    void Update()
    {
        // Check for mouse input or touch input
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            PerformRaycast(ray);
        }
        else if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
            PerformRaycast(ray);
        }
    }

    void PerformRaycast(Ray ray)
    {
        // Check for collision with an object
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

            if (hit.collider.CompareTag("Cell"))
            {
                currentCell = hit.collider.GetComponent<_09Cell>();
                if(gameLogic.setFlags){
                    currentCell.SetFlags();
                    return;
                }
                
                if (currentCell.isMine)
                {
                    currentCell.ChangeColor(Color.red);
                    this.enabled=false;
                    Popup popup=FindObjectOfType<Popup>();
                    popup.OnActivate("RESTART?");
                    return;
                }

                currentCell.ChangeColor(Color.white);
                if (lastCell)
                {
                    lastCell.ResetColor();
                }
                else
                {
                    gameLogic.PlaceMines(gameLogic.mineCount, currentCell);
                }
                lastCell = currentCell;
                gameLogic.RevealEmptyCells(currentCell.X,currentCell.Y);
            }
            // else
            // {
            //     Debug.Log("NO ENEMY HIT");
            // }
        }

        // else
        // {
        //     Debug.Log("NO COLLIDER DETECTED");
        // }
    }
}
