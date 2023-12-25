using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class _09GameLogic : MonoBehaviour
{
    [SerializeField] private int length = 9;
    [SerializeField] private int width = 9;
    public _09Cell[,] cells;
    public bool setFlags = false;
    public Image img;
    public int mineCount = 10;
    public Slider mineCountSlider;
    public TextMeshProUGUI sliderCountText;

    public void OnSliderChange()
    {
        mineCount = Convert.ToInt32(mineCountSlider.value);
        sliderCountText.text = mineCount.ToString();
    }
    public void SetFlags()
    {
        setFlags = !setFlags;
        if (setFlags) img.color = Color.red;
        else img.color = Color.cyan;
    }
    private void Start()
    {
        cells = new _09Cell[length, width];
    }
    public void AddCell(_09Cell c, int i, int j)
    {
        cells[i, j] = c;
    }
    public void PlaceMines(int mineCount, _09Cell excludeCell)
    {

        // Flatten the 2D array to simplify random indexing
        _09Cell[] flatCells = new _09Cell[width * length];
        int index = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                flatCells[index++] = cells[i, j];
            }
        }

        // Shuffle the array to randomize mine placement
        for (int i = flatCells.Length - 1; i > 0; i--)
        {
            int randIndex = UnityEngine.Random.Range(0, i + 1);
            _09Cell temp = flatCells[i];
            flatCells[i] = flatCells[randIndex];
            flatCells[randIndex] = temp;
        }

        // Place mines, excluding the specified cell
        int minesPlaced = 0;
        foreach (_09Cell cell in flatCells)
        {
            if (cell != excludeCell)
            {
                cell.isMine = true;
                minesPlaced++;

                if (minesPlaced >= mineCount)
                {
                    // Stop placing mines once the desired count is reached
                    break;
                }
            }
        }
    }

    private bool showingMines = false;
    public void ToggleMinesVisibility()
    {
        int width = cells.GetLength(0);
        int height = cells.GetLength(1);
        if (showingMines)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    _09Cell cell = cells[i, j];
                    if (cell.isMine) cell.ResetColor();

                }
            }
            showingMines = !showingMines;
            return;
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                _09Cell cell = cells[i, j];
                if (cell.isMine) cell.ChangeColor(Color.cyan);
            }
        }
        showingMines = !showingMines;
    }
    public void RevealEmptyCells(int i, int j)
    {
        // Check bounds to avoid array out-of-bounds errors
        if (i < 0 || i >= 9 || j < 0 || j >= 9)
            return;

        _09Cell cell = cells[i, j];
        int mines = CountMineNeighbours(i, j);
        cell.SetCellNeighbors(mines);
        // Mark the cell as revealed


        // Check if the cell is already revealed or has a mine
        if (!cell.isRevealed && !cell.isMine)
        {

            cell.isRevealed = true;
            if (CheckWinCondition())
            {
                Popup popup = FindObjectOfType<Popup>();
                popup.OnActivate("YOU WON ,RESTART?");
                this.enabled=false;
                return;
            }
            if (mines > 0) return;
            // cell.SetCellNeighbors();
            // Assuming _09Cell has a Renderer component
            Renderer cellRenderer = cell.GetComponent<Renderer>();
            cellRenderer.enabled = true;

            // Check if the cell has no adjacent mines
            if (cell.surroundingMines == 0)
            {
                cell.ChangeColor(Color.white);
                // Recursively reveal neighboring cells
                RevealEmptyCells(i - 1, j); // Left
                RevealEmptyCells(i + 1, j); // Right
                RevealEmptyCells(i, j - 1); // Down
                RevealEmptyCells(i, j + 1); // Up
                RevealEmptyCells(i - 1, j - 1); // Bottom-left
                RevealEmptyCells(i - 1, j + 1); // Top-left
                RevealEmptyCells(i + 1, j - 1); // Bottom-right
                RevealEmptyCells(i + 1, j + 1); // Top-right

            }
        }
    }
    int CountMineNeighbours(int x, int y)
    {
        int mineCount = 0;

        // Iterate over the neighboring cells
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                // Check bounds to avoid array out-of-bounds errors
                if (i >= 0 && i < 9 && j >= 0 && j < 9)
                {
                    // Check if the current cell has a mine
                    if (cells[i, j].isMine)
                    {
                        mineCount++;
                    }
                }
            }
        }

        return mineCount;
    }
    public bool CheckWinCondition()
    {
        int width = 9;
        int height = 9;

        // Iterate through all cells
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                _09Cell cell = cells[i, j];

                // Check if a non-mine cell is not revealed
                if (!cell.isMine && !cell.isRevealed)
                {
                    // The win condition is not met
                    return false;
                }
            }
        }

        // The win condition is met
        return true;
    }
}
