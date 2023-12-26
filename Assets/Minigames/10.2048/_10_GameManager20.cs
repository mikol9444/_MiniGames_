using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _10_GameManager20 : MonoBehaviour
{
    public GameObject cellPrefab;
    public _10_Cell[,] myCellArray = new _10_Cell[4, 4];

    public _10_Cell[,] tmp = new _10_Cell[4, 4];
    public bool isInitialized = false;
    public float debugTimer = 0.5f;
    private void Start()
    {
        CreateBoard();
        SpawnFirstCell();

    }
    public _10_Cell[,] CopyCellArray()
    {

        _10_Cell[,] newArray = new _10_Cell[4, 4];

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                // Create a new instance for each element
                newArray[i, j] = tmp[i, j];
                newArray[i, j].CurrentNumber = myCellArray[i, j].CurrentNumber;
            }
        }

        return newArray;
    }
    public bool AreArraysEqual(_10_Cell[,] array1, _10_Cell[,] array2)
    {

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                // Compare the CurrentNumber property of each element
                if (array1[i, j].CurrentNumber != array2[i, j].CurrentNumber)
                {
                    return false;
                }
            }
        }

        // All elements are equal
        return true;
    }

    private void CreateBoard()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Vector3 spawnPosition = new Vector3(i, 0, j);
                GameObject obj = ObjectPoolManager.SpawnObject(cellPrefab, spawnPosition, Quaternion.identity, PoolType.GameObject);
                _10_Cell cell = obj.GetComponent<_10_Cell>();
                myCellArray[i, j] = cell;
                // myCellArray[i, j].Initialize(0, i, j);
            }
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Vector3 spawnPosition = new Vector3(i + 5, 0, j);
                GameObject obj = ObjectPoolManager.SpawnObject(cellPrefab, spawnPosition, Quaternion.identity, PoolType.GameObject);
                _10_Cell cell = obj.GetComponent<_10_Cell>();
                tmp[i, j] = cell;
                // tmp[i, j].Initialize(0, i, j);
            }
        }
                
    }
    public void SpawnFirstCell()
    {

        List<Vector2Int> arrayElements = new List<Vector2Int>();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (myCellArray[i, j].CurrentNumber == 0) arrayElements.Add(new Vector2Int(i, j));
            }
        }
        if (arrayElements.Count > 0)
        {
            int randomIndex = Random.Range(0, arrayElements.Count);
            Vector2Int v = arrayElements[randomIndex];
            myCellArray[v.x, v.y].CurrentNumber = 2;
        }
    }
    public void SpawnCell()
    {
        
        if (AreArraysEqual(tmp, myCellArray)) return;
        List<Vector2Int> arrayElements = new List<Vector2Int>();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (myCellArray[i, j].CurrentNumber == 0) arrayElements.Add(new Vector2Int(i, j));
            }
        }
        if (arrayElements.Count > 0)
        {
            int randomIndex = Random.Range(0, arrayElements.Count);
            Vector2Int v = arrayElements[randomIndex];
            myCellArray[v.x, v.y].CurrentNumber = 2;
        }
    }
    public IEnumerator MergeCellsLeft()
    {
        tmp = CopyCellArray();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++) // Note: We go up to 3 to avoid checking the last column
            {
                if (myCellArray[j, i].CurrentNumber > 0)
                {
                    int targetIndex = j + 1;

                    while (targetIndex < 4 && myCellArray[targetIndex, i].CurrentNumber == 0)
                    {
                        targetIndex++;
                        Debug.Log($"Checking ({j},{i}): TargetIndex: {targetIndex}");
                    }

                    if (targetIndex < 4 && myCellArray[targetIndex, i].CurrentNumber == myCellArray[j, i].CurrentNumber)
                    {
                        // Merge cells
                        myCellArray[targetIndex, i].CurrentNumber *= 2;
                        myCellArray[j, i].CurrentNumber = 0;
                        yield return new WaitForSeconds(debugTimer);
                    }
                }
            }
        }

        // Reset colors after merging
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                myCellArray[j, i].GetComponent<Renderer>().material.color = Color.grey; // Debugging
            }
        }
        StartCoroutine(MoveCellsLeft());
    }
    public IEnumerator MoveCellsLeft()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                if (myCellArray[j, i].CurrentNumber > 0)
                {
                    int targetIndex = j;
                    myCellArray[j, i].GetComponent<Renderer>().material.color = Color.red; //Debugging
                    while (targetIndex > 0 && myCellArray[targetIndex - 1, i].CurrentNumber == 0)
                    {
                        targetIndex--;
                        Debug.Log($"Checking ({j},{i}):Targetindex: {targetIndex}");
                    }
                    if (targetIndex != j)
                    {
                        myCellArray[targetIndex, i].CurrentNumber = myCellArray[j, i].CurrentNumber;
                        myCellArray[j, i].CurrentNumber = 0;


                    }
                    yield return new WaitForSeconds(debugTimer);
                }
            }
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                myCellArray[j, i].GetComponent<Renderer>().material.color = Color.grey; //Debugging
            }
        }
        SpawnCell();
    }
    public IEnumerator MergeCellsRight()
    {
        tmp = CopyCellArray();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 3; j > 0; j--) // Note: We go down to 1 to avoid checking the first column
            {
                if (myCellArray[j, i].CurrentNumber > 0)
                {
                    int targetIndex = j - 1;

                    while (targetIndex >= 0 && myCellArray[targetIndex, i].CurrentNumber == 0)
                    {
                        targetIndex--;
                        Debug.Log($"Checking ({j},{i}): TargetIndex: {targetIndex}");
                    }

                    if (targetIndex >= 0 && myCellArray[targetIndex, i].CurrentNumber == myCellArray[j, i].CurrentNumber)
                    {
                        // Merge cells
                        myCellArray[targetIndex, i].CurrentNumber *= 2;
                        myCellArray[j, i].CurrentNumber = 0;
                        yield return new WaitForSeconds(debugTimer);
                    }
                }
            }
        }

        // Reset colors after merging
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                myCellArray[j, i].GetComponent<Renderer>().material.color = Color.grey; // Debugging
            }
        }
        StartCoroutine(MoveCellsRight());
    }

    public IEnumerator MoveCellsRight()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 2; j >= 0; j--)
            {
                if (myCellArray[j, i].CurrentNumber > 0)
                {
                    int targetIndex = j;
                    myCellArray[j, i].GetComponent<Renderer>().material.color = Color.red; //Debugging
                    while (targetIndex < 3 && myCellArray[targetIndex + 1, i].CurrentNumber == 0)
                    {
                        targetIndex++;
                        Debug.Log($"Checking ({j},{i}): Targetindex: {targetIndex}");
                    }
                    if (targetIndex != j)
                    {
                        myCellArray[targetIndex, i].CurrentNumber = myCellArray[j, i].CurrentNumber;
                        myCellArray[j, i].CurrentNumber = 0;
                    }
                    yield return new WaitForSeconds(debugTimer);
                }
            }
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                myCellArray[j, i].GetComponent<Renderer>().material.color = Color.grey; //Debugging
            }
        }
        SpawnCell();
    }
    public IEnumerator MergeCellsUp()
    {
        tmp = CopyCellArray();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 3; j > 0; j--) // Note: We go down to 1 to avoid checking the first row
            {
                if (myCellArray[i, j].CurrentNumber > 0)
                {
                    int targetIndex = j - 1;

                    while (targetIndex >= 0 && myCellArray[i, targetIndex].CurrentNumber == 0)
                    {
                        targetIndex--;
                        Debug.Log($"Checking ({i},{j}): TargetIndex: {targetIndex}");
                    }

                    if (targetIndex >= 0 && myCellArray[i, targetIndex].CurrentNumber == myCellArray[i, j].CurrentNumber)
                    {
                        // Merge cells
                        myCellArray[i, targetIndex].CurrentNumber *= 2;
                        myCellArray[i, j].CurrentNumber = 0;
                        yield return new WaitForSeconds(debugTimer);
                    }
                }
            }
        }

        // Reset colors after merging
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                myCellArray[i, j].GetComponent<Renderer>().material.color = Color.grey; // Debugging
            }
        }
        StartCoroutine(MoveCellsUp());
    }

    public IEnumerator MoveCellsUp()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 2; j >= 0; j--)
            {
                if (myCellArray[i, j].CurrentNumber > 0)
                {
                    int targetIndex = j;
                    myCellArray[i, j].GetComponent<Renderer>().material.color = Color.red; //Debugging
                    while (targetIndex < 3 && myCellArray[i, targetIndex + 1].CurrentNumber == 0)
                    {
                        targetIndex++;
                        Debug.Log($"Checking ({i},{j}): Targetindex: {targetIndex}");
                    }
                    if (targetIndex != j)
                    {
                        myCellArray[i, targetIndex].CurrentNumber = myCellArray[i, j].CurrentNumber;
                        myCellArray[i, j].CurrentNumber = 0;
                    }
                    yield return new WaitForSeconds(debugTimer);
                }
            }
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                myCellArray[i, j].GetComponent<Renderer>().material.color = Color.grey; //Debugging
            }
        }
        SpawnCell();
    }
    public IEnumerator MergeCellsDown()
    {
        tmp = CopyCellArray();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++) // Note: We go up to 3 to avoid checking the last row
            {
                if (myCellArray[i, j].CurrentNumber > 0)
                {
                    int targetIndex = j + 1;

                    while (targetIndex < 4 && myCellArray[i, targetIndex].CurrentNumber == 0)
                    {
                        targetIndex++;
                        Debug.Log($"Checking ({i},{j}): TargetIndex: {targetIndex}");
                    }

                    if (targetIndex < 4 && myCellArray[i, targetIndex].CurrentNumber == myCellArray[i, j].CurrentNumber)
                    {
                        // Merge cells
                        myCellArray[i, targetIndex].CurrentNumber *= 2;
                        myCellArray[i, j].CurrentNumber = 0;
                        yield return new WaitForSeconds(debugTimer);
                    }
                }
            }
        }

        // Reset colors after merging
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                myCellArray[i, j].GetComponent<Renderer>().material.color = Color.grey; // Debugging
            }
        }
        StartCoroutine(MoveCellsDown());
    }

    public IEnumerator MoveCellsDown()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                if (myCellArray[i, j].CurrentNumber > 0)
                {
                    int targetIndex = j;
                    myCellArray[i, j].GetComponent<Renderer>().material.color = Color.red; //Debugging
                    while (targetIndex > 0 && myCellArray[i, targetIndex - 1].CurrentNumber == 0)
                    {
                        targetIndex--;
                        Debug.Log($"Checking ({i},{j}): Targetindex: {targetIndex}");
                    }
                    if (targetIndex != j)
                    {
                        myCellArray[i, targetIndex].CurrentNumber = myCellArray[i, j].CurrentNumber;
                        myCellArray[i, j].CurrentNumber = 0;
                    }
                    yield return new WaitForSeconds(debugTimer);
                }
            }
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                myCellArray[i, j].GetComponent<Renderer>().material.color = Color.grey; //Debugging
            }
        }
        SpawnCell();
    }


}
