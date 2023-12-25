using System.Collections;
using System.Collections.Generic;
using Essentials;
using UnityEngine;
using UnityEngine.EventSystems;
public enum Direction
{
    Left,
    Right,
    Up,
    Down
}
public class _10_GameManager : MonoBehaviour
{
    public GameObject cellPrefab;
    bool[,] myBoolArray = new bool[4, 4];
    private _10_Cell[,] CellArray = new _10_Cell[4, 4];

    private void Start()
    {
        // CreateBoard();

        SpawnCell();
    }
    public void SpawnCell()
    {
        Vector2Int vec = GetRandomFalseElement();
        Vector3 spawnPos = new Vector3(vec.x, 0, vec.y);
        GameObject obj = ObjectPoolManager.SpawnObject(cellPrefab, spawnPos, Quaternion.identity, PoolType.GameObject);
        myBoolArray[vec.x, vec.y] = true;
        CellArray[vec.x, vec.y] = obj.GetComponent<_10_Cell>();
    }
    public GameObject SpawnCell(int i, int j)
    {
        Vector3 spawnPos = new Vector3(i, 0, j);
        GameObject obj = ObjectPoolManager.SpawnObject(cellPrefab, spawnPos, Quaternion.identity, PoolType.GameObject);
        myBoolArray[i, j] = true;
        CellArray[i, j] = obj.GetComponent<_10_Cell>();
        return obj;
    }
    Vector2Int GetRandomFalseElement()
    {


        // Create a list to store the indices of false elements
        var falseIndices = new List<Vector2Int>();

        // Find all false elements and store their indices
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (!myBoolArray[row, col])
                {
                    falseIndices.Add(new Vector2Int(row, col));
                }
            }
        }

        // Check if there are any false elements
        if (falseIndices.Count > 0)
        {
            // Randomly select one false element
            int randomIndex = Random.Range(0, falseIndices.Count);
            return falseIndices[randomIndex];
        }
        else
        {
            // No false elements found
            return Vector2Int.zero;
        }
    }
    public void MoveCells(MoveDirection dir)
    {
        switch (dir)
        {
            case MoveDirection.Up:
                MoveCellsUp();
                break;
            case MoveDirection.Down:
                MoveCellsDown();
                break;
            case MoveDirection.Right:
                MoveCellsRight();
                break;
            case MoveDirection.Left:
                MoveCellsLeft();
                break;

        }
    }
   void MoveCellsLeft()
{
    for (int j = 0; j < 4; j++)
    {
        for (int i = 1; i < 4; i++)
        {
            if (CellArray[i, j])
            {
                int targetIndex = i;
                while (targetIndex > 0 && CellArray[targetIndex - 1, j] == null)
                {
                    targetIndex--;
                }

                if (targetIndex != i)
                {
                    CellArray[targetIndex, j] = CellArray[i, j];
                    CellArray[i, j] = null;
                    CellArray[targetIndex, j].transform.position = new Vector3(targetIndex, 0, j);
                }
            }
        }
    }
}

void MoveCellsRight()
{
    for (int j = 0; j < 4; j++)
    {
        for (int i = 2; i >= 0; i--)
        {
            if (CellArray[i, j])
            {
                int targetIndex = i;
                while (targetIndex < 3 && CellArray[targetIndex + 1, j] == null)
                {
                    targetIndex++;
                }

                if (targetIndex != i)
                {
                    CellArray[targetIndex, j] = CellArray[i, j];
                    CellArray[i, j] = null;
                    CellArray[targetIndex, j].transform.position = new Vector3(targetIndex, 0, j);
                }
            }
        }
    }
}
void MoveCellsUp()
{
    for (int i = 0; i < 4; i++)
    {
        for (int j = 3; j > 0; j--)
        {
            if (CellArray[i, j] != null)
            {
                int targetIndex = j;
                while (targetIndex > 0 && CellArray[i, targetIndex - 1] == null)
                {
                    targetIndex--;
                }

                if (targetIndex != j)
                {
                    CellArray[i, targetIndex] = CellArray[i, j];
                    CellArray[i, j] = null;
                    CellArray[i, targetIndex].transform.position = new Vector3(i, 0, targetIndex);
                }
            }
        }
    }
}

void MoveCellsDown()
{
    for (int i = 0; i < 4; i++)
    {
        for (int j = 0; j < 3; j++)
        {
            if (CellArray[i, j] != null)
            {
                int targetIndex = j;
                while (targetIndex < 3 && CellArray[i, targetIndex + 1] == null)
                {
                    targetIndex++;
                }

                if (targetIndex != j)
                {
                    CellArray[i, targetIndex] = CellArray[i, j];
                    CellArray[i, j] = null;
                    CellArray[i, targetIndex].transform.position = new Vector3(i, 0, targetIndex);
                }
            }
        }
    }
}




}
