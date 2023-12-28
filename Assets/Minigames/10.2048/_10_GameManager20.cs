using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class _10_GameManager20 : MonoBehaviour
{
    public int size=4;
    public GameObject cellPrefab;
    public _10_Cell[,] myCellArray;
    public Slider slider;
    public int[,] tmp;
    public bool isInitialized = false;
    public float debugTimer = 0.5f;
    private void Start() {}
    public void OnSliderValueChange(){
        size = Convert.ToInt32(slider.value);
        slider.GetComponentInChildren<TextMeshProUGUI>().text = slider.value.ToString();
    }
        // Function to check if every element has a different number compared to its neighbors
bool CheckAllDifferent()
{
    if(myCellArray.Cast<_10_Cell>().Any(cell => cell.CurrentNumber == 0)){
        return false;
    }

    for (int i = 0; i < size; i++)
    {
        for (int j = 0; j < size; j++)
        {
            // Check neighbors to the right
            if (j + 1 < size && myCellArray[i,j].CurrentNumber == myCellArray[i, j + 1].CurrentNumber)
            {
                return false;
            }

            // Check neighbors below
            if (i + 1 < size && myCellArray[i,j].CurrentNumber == myCellArray[i + 1, j].CurrentNumber)
            {
                return false;
            }

            // Check neighbors to the left
            if (j - 1 >= 0 && myCellArray[i,j].CurrentNumber == myCellArray[i, j - 1].CurrentNumber)
            {
                return false;
            }

            // Check neighbors above
            if (i - 1 >= 0 && myCellArray[i,j].CurrentNumber == myCellArray[i - 1, j].CurrentNumber)
            {
                return false;
            }
        }
    }

    // All elements have different neighbors
    return true;
}
    public void CreateBoard()
    {
        myCellArray = new _10_Cell[size, size];
        tmp = new int[size, size];
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
            {
                GameObject obj = ObjectPoolManager.SpawnObject(cellPrefab, new Vector3(i, 0, j), Quaternion.identity, PoolType.GameObject);
                _10_Cell cell = obj.GetComponent<_10_Cell>();
                myCellArray[i, j] = cell; tmp[i, j] = myCellArray[i, j].CurrentNumber;    
            }
            Camera cam = Camera.main;
            cam.transform.position = new Vector3(cam.transform.position.x,cam.transform.position.y+(size/2+2),cam.transform.position.z+(size/2-1.5f));

    }
    public void SpawnFirstCell()
    {
        List<Vector2Int> arrayElements = new List<Vector2Int>();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (myCellArray[i, j].CurrentNumber == 0) arrayElements.Add(new Vector2Int(i, j));
            }
        }
        if (arrayElements.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, arrayElements.Count);
            Vector2Int v = arrayElements[randomIndex];
            myCellArray[v.x, v.y].CurrentNumber = 2;
        }
    }
    public void SpawnCell()
    {

        if (AreArraysEqual()) return;
        List<Vector2Int> arrayElements = new List<Vector2Int>();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (myCellArray[i, j].CurrentNumber == 0) arrayElements.Add(new Vector2Int(i, j));
            }
        }
        if (arrayElements.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, arrayElements.Count);
            Vector2Int v = arrayElements[randomIndex];
            myCellArray[v.x, v.y].CurrentNumber = 2;
        }
    }
        public void MoveTheCells(SwipeDirection swipeDirection)
    {
        switch (swipeDirection)
        {
            case SwipeDirection.RIGHT:
                // Handle right swipe
                MergeCellsRight();
                break;
            case SwipeDirection.LEFT:
                // Handle left swipe
                MergeCellsLeft();
                break;
            case SwipeDirection.UP:
                // Handle up swipe
                MergeCellsUp();
                break;
            case SwipeDirection.DOWN:
                // Handle down swipe
                MergeCellsDown();
                break;
            default:
                // Handle any other cases or provide a default action
                break;
        }
        int num = size>8? 4096:2048;
        if (size>12) num = 8192;
        if(Contains2048(num)){
            Debug.LogWarning("YOU WON!?!");
            FindObjectOfType<Popup>().OnActivate("YOU WON! RESTART?");
            FindObjectOfType<_10_SwipeDetector>().enabled=false;
            FindObjectOfType<_10_SwipeDetector>().StopSwiping();
            FindObjectOfType<_10_SwipeDetector>().StopSwiping2();
            this.enabled = false;
        }
        ChangeCellColors();
        if(CheckAllDifferent()){
            Debug.LogWarning("YOU LOST!?!");
            FindObjectOfType<Popup>().OnActivate("YOU LOST! RESTART?");
            FindObjectOfType<_10_SwipeDetector>().StopSwiping();
            FindObjectOfType<_10_SwipeDetector>().StopSwiping2();
            FindObjectOfType<_10_SwipeDetector>().enabled=false;
            this.enabled = false;
        }
    }
    private void ChangeCellColors(){
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
            {
                myCellArray[i,j].UpdateCellColor();
            }
            
        
    }

    #region GAMELOGIC
        public bool Contains2048(int number)
    {
        // Check if any element in the 2D array equals 2048
        return myCellArray.Cast<_10_Cell>().Any(x => x.CurrentNumber == number);
    }
    public void CopyCellArray()
    {
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                tmp[i, j] = myCellArray[i, j].CurrentNumber;
    }
    public bool AreArraysEqual()
    {
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                if (tmp[i, j] != myCellArray[i, j].CurrentNumber) return false;  // Compare the CurrentNumber property of each elemens
        return true;         // All elements are equal
    }

    public void MergeCellsLeft()
    {
        CopyCellArray();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size-1; j++) // Note: We go up to 3 to avoid checking the last column
            {
                if (myCellArray[j, i].CurrentNumber > 0)
                {
                    int targetIndex = j + 1;

                    while (targetIndex < size && myCellArray[targetIndex, i].CurrentNumber == 0)
                    {
                        targetIndex++;
                        // Debug.Log($"Checking ({j},{i}): TargetIndex: {targetIndex}");
                    }

                    if (targetIndex < size && myCellArray[targetIndex, i].CurrentNumber == myCellArray[j, i].CurrentNumber)
                    {
                        // Merge cells
                        myCellArray[targetIndex, i].CurrentNumber *= 2;
                        myCellArray[j, i].CurrentNumber = 0;

                    }
                }
            }
        }

        MoveCellsLeft();
    }
    public void MoveCellsLeft()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 1; j < size; j++)
            {
                if (myCellArray[j, i].CurrentNumber > 0)
                {
                    int targetIndex = j;
                    // myCellArray[j, i].GetComponent<Renderer>().material.color = Color.red; //Debugging
                    while (targetIndex > 0 && myCellArray[targetIndex - 1, i].CurrentNumber == 0)
                    {
                        targetIndex--;
                        // Debug.Log($"Checking ({j},{i}):Targetindex: {targetIndex}");
                    }
                    if (targetIndex != j)
                    {
                        myCellArray[targetIndex, i].CurrentNumber = myCellArray[j, i].CurrentNumber;
                        myCellArray[j, i].CurrentNumber = 0;


                    }

                }
            }
        }
        SpawnCell();
    }
    public void MergeCellsRight()
    {
        CopyCellArray();
        for (int i = 0; i < size; i++)
        {
            for (int j = size-1; j > 0; j--) // Note: We go down to 1 to avoid checking the first column
            {
                if (myCellArray[j, i].CurrentNumber > 0)
                {
                    int targetIndex = j - 1;

                    while (targetIndex >= 0 && myCellArray[targetIndex, i].CurrentNumber == 0)
                    {
                        targetIndex--;
                        // Debug.Log($"Checking ({j},{i}): TargetIndex: {targetIndex}");
                    }

                    if (targetIndex >= 0 && myCellArray[targetIndex, i].CurrentNumber == myCellArray[j, i].CurrentNumber)
                    {
                        // Merge cells
                        myCellArray[targetIndex, i].CurrentNumber *= 2;
                        myCellArray[j, i].CurrentNumber = 0;

                    }
                }
            }
        }

        MoveCellsRight();
    }
    public void MoveCellsRight()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = size-1; j >= 0; j--)
            {
                if (myCellArray[j, i].CurrentNumber > 0)
                {
                    int targetIndex = j;
                    while (targetIndex < size-1 && myCellArray[targetIndex + 1, i].CurrentNumber == 0)
                    {
                        targetIndex++;
                        // Debug.Log($"Checking ({j},{i}): Targetindex: {targetIndex}");
                    }
                    if (targetIndex != j)
                    {
                        myCellArray[targetIndex, i].CurrentNumber = myCellArray[j, i].CurrentNumber;
                        myCellArray[j, i].CurrentNumber = 0;
                    }

                }
            }
        }
        SpawnCell();
    }
    public void MergeCellsUp()
    {
        CopyCellArray();
        for (int i = 0; i < size; i++)
        {
            for (int j = size-1; j > 0; j--) // Note: We go down to 1 to avoid checking the first row
            {
                if (myCellArray[i, j].CurrentNumber > 0)
                {
                    int targetIndex = j - 1;

                    while (targetIndex >= 0 && myCellArray[i, targetIndex].CurrentNumber == 0)
                    {
                        targetIndex--;
                        // Debug.Log($"Checking ({i},{j}): TargetIndex: {targetIndex}");
                    }

                    if (targetIndex >= 0 && myCellArray[i, targetIndex].CurrentNumber == myCellArray[i, j].CurrentNumber)
                    {
                        // Merge cells
                        myCellArray[i, targetIndex].CurrentNumber *= 2;
                        myCellArray[i, j].CurrentNumber = 0;

                    }
                }
            }
        }

        MoveCellsUp();
    }
    public void MoveCellsUp()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = size-1; j >= 0; j--)
            {
                if (myCellArray[i, j].CurrentNumber > 0)
                {
                    int targetIndex = j;
                    while (targetIndex < size-1 && myCellArray[i, targetIndex + 1].CurrentNumber == 0)
                    {
                        targetIndex++;
                        // Debug.Log($"Checking ({i},{j}): Targetindex: {targetIndex}");
                    }
                    if (targetIndex != j)
                    {
                        myCellArray[i, targetIndex].CurrentNumber = myCellArray[i, j].CurrentNumber;
                        myCellArray[i, j].CurrentNumber = 0;
                    }

                }
            }
        }
        SpawnCell();
    }
    public void MergeCellsDown()
    {
        CopyCellArray();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size-1; j++) // Note: We go up to 3 to avoid checking the last row
            {
                if (myCellArray[i, j].CurrentNumber > 0)
                {
                    int targetIndex = j + 1;

                    while (targetIndex < size && myCellArray[i, targetIndex].CurrentNumber == 0)
                    {
                        targetIndex++;
                        // Debug.Log($"Checking ({i},{j}): TargetIndex: {targetIndex}");
                    }

                    if (targetIndex < size && myCellArray[i, targetIndex].CurrentNumber == myCellArray[i, j].CurrentNumber)
                    {
                        // Merge cells
                        myCellArray[i, targetIndex].CurrentNumber *= 2;
                        myCellArray[i, j].CurrentNumber = 0;

                    }
                }
            }
        }

        MoveCellsDown();
    }
    public void MoveCellsDown()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 1; j < size; j++)
            {
                if (myCellArray[i, j].CurrentNumber > 0)
                {
                    int targetIndex = j;
                    while (targetIndex > 0 && myCellArray[i, targetIndex - 1].CurrentNumber == 0)
                    {
                        targetIndex--;
                        // Debug.Log($"Checking ({i},{j}): Targetindex: {targetIndex}");
                    }
                    if (targetIndex != j)
                    {
                        myCellArray[i, targetIndex].CurrentNumber = myCellArray[i, j].CurrentNumber;
                        myCellArray[i, j].CurrentNumber = 0;
                    }

                }
            }
        }
        SpawnCell();
    }
    #endregion GAMELOGIC
}
