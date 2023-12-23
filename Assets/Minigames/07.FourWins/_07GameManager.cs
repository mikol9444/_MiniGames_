using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class _07GameManager : MonoBehaviour
{
    public Transform arrowTransform;
    public int arrowPosition =1;
    public bool playerOnesTurn = true;
    public TextMeshProUGUI topLabel;
    public bool[][] bool2DArray;
    public bool[][] bool2DArray_Player1;
    public bool[][] bool2DArray_Player2;
    public int numRows = 8; // Number of rows
    public int numCols = 7; // Number of columns
    public GameObject player1Coin,player2Coin;
       
     [SerializeField]private Button yourButton;
    private void Start() {
        InitArray();
    }
    public void InitArray(){
            // Initialize the 2D boolean array with false values


    bool2DArray = new bool[numRows][];
    bool2DArray_Player1=new bool[numRows][];
    bool2DArray_Player2=new bool[numRows][];
    for (int i = 0; i < numRows; i++)
    {
        bool2DArray_Player1[i]=new bool[numCols];
        bool2DArray_Player2[i]=new bool[numCols];
        bool2DArray[i] = new bool[numCols];
        for (int j = 0; j < numCols; j++)
        {
            bool2DArray[i][j] = false;
            bool2DArray_Player1[i][j]=false;
            bool2DArray_Player2[i][j]=false;
        }
    }
    }
public void MoveArrowRight(){
    if (arrowTransform.position.x<12)
    {
        arrowTransform.position += new Vector3(2,0,0);
        arrowPosition+=1;
    }
}
public void MoveArrowLeft(){
    if (arrowTransform.position.x>0)
    {
        arrowTransform.position += new Vector3(-2,0,0);
        arrowPosition-=1;
    }
}
public void Move(){



    for (int i = 0; i < numRows; i++)
    {

        if (!bool2DArray[i][arrowPosition-1]&&i<numRows)
        {
                 GameObject coin = playerOnesTurn? player1Coin : player2Coin;
    GameObject obj = Instantiate(coin,arrowTransform.position,coin.transform.rotation);
            Debug.Log($"Looping... +{i} {arrowPosition-1}");
            Vector3 destination = new Vector3((arrowPosition-1)*2,i*2+1,0);
            obj.GetComponent<_07Animator>().StartMovement(destination);
            bool2DArray[i][arrowPosition-1]=true;
            if (playerOnesTurn)
            {
                bool2DArray_Player1[i][arrowPosition-1]=true;
                 Debug.Log(CheckForWin(bool2DArray_Player1));
                 if (CheckForWin(bool2DArray_Player1))
                 {
                    FindObjectOfType<Popup>().OnActivate("player 1 won - restart?");
                    yourButton.interactable = false;
                 }
            }
            else{
                bool2DArray_Player2[i][arrowPosition-1]=true;
                if (CheckForWin(bool2DArray_Player2))
                 {
                    FindObjectOfType<Popup>().OnActivate("player 2 won - restart?");
                    yourButton.interactable = false;
                 }
            }
            playerOnesTurn=!playerOnesTurn;
            topLabel.text = playerOnesTurn? "Player 1 Turn" : "Player 2 Turn";
            break;
        }
    }
   
          
}
 // Function to check for a win condition
    public bool CheckForWin(bool[][] boolArray)
    {

        // Check horizontal
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols - 3; col++)
            {
                if (boolArray[row][col] &&
                    boolArray[row][col + 1] &&
                    boolArray[row][col + 2] &&
                    boolArray[row][col + 3])
                {
                    return true; // Found a win
                }
            }
        }

        // Check vertical
        for (int row = 0; row < numRows - 3; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                if (boolArray[row][col] &&
                    boolArray[row + 1][col] &&
                    boolArray[row + 2][col] &&
                    boolArray[row + 3][col])
                {
                    return true; // Found a win
                }
            }
        }

        // Check diagonal (from bottom-left to top-right)
        for (int row = 3; row < numRows; row++)
        {
            for (int col = 0; col < numCols - 3; col++)
            {
                if (boolArray[row][col] &&
                    boolArray[row - 1][col + 1] &&
                    boolArray[row - 2][col + 2] &&
                    boolArray[row - 3][col + 3])
                {
                    return true; // Found a win
                }
            }
        }

        // Check diagonal (from top-left to bottom-right)
        for (int row = 0; row < numRows - 3; row++)
        {
            for (int col = 0; col < numCols - 3; col++)
            {
                if (boolArray[row][col] &&
                    boolArray[row + 1][col + 1] &&
                    boolArray[row + 2][col + 2] &&
                    boolArray[row + 3][col + 3])
                {
                    return true; // Found a win
                }
            }
        }

        return false; // No win condition found
    }
}
