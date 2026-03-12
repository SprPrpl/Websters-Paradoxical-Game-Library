using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
public class Game : MonoBehaviour
{
    public static Game Singleton;
    private bool isPlayerTurn;
    public Transform buttons;
    public char[,] board;
    public int[,] AIBoardPositive;
    public int[,] AIBoardNegative;

    public List<Vector2> xMoves;
    public List<Vector2> oMoves;
    public int currentMove;
    private void Awake()
    {
        isPlayerTurn = true;
        currentMove = 0;
        board = new char[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = '-';
            }
        }
    }

    public void Start()
    {
        Singleton = this;
    }

    private void drawBoard()
    {
        int n = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == '-')
                {
                    foreach (Transform t in buttons.GetChild(n))
                        t.gameObject.SetActive(false);
                }
                else if (board[i, j] == 'X')
                {
                    buttons.GetChild(n).GetChild(0).gameObject.SetActive(true);
                    buttons.GetChild(n).GetChild(1).gameObject.SetActive(false);
                } else if (board[i, j] == 'O')
                {
                    buttons.GetChild(n).GetChild(0).gameObject.SetActive(false);
                    buttons.GetChild(n).GetChild(1).gameObject.SetActive(true);
                }

                n++;
            }
        }
    }
    public void switchTurn()
    {
        drawBoard();
        isPlayerTurn = !isPlayerTurn;
        enableButtons(isPlayerTurn);
        if (!isPlayerTurn)
        {
            Vector2 move = CheckWin();
            Debug.Log(move.ToSafeString());
            if (board[(int)move.x, (int)move.y] != '-')
                Debug.Log("CAnt dothat moron");
            board[(int)move.x, (int)move.y] = 'X';
            switchTurn();
        }

    }



    public void enableButtons(bool b)
    {
        foreach (Transform t in buttons)
        {
            t.GetComponent<BoxCollider2D>().enabled = b;
        }
    }

    public void playerSelected(int[] position)
    {
        board[position[0], position[1]] = 'O';
        switchTurn();
    }

    public Vector2 CheckWin()
    {
        AIBoardPositive = new int[3, 3];
        AIBoardNegative = new int[3, 3];

        int[] slice;
        int xCount;
        int oCount;

        //check verticles
        for(int j = 0; j < 3; j++)
        {
            slice = new int[3];
            xCount = 0;
            oCount = 0;
            for (int i = 0; i < 3; i++)
            {
                if (board[i, j] == 'X')
                    xCount++;
                else if (board[i, j] == 'O')
                    oCount++;
                else
                    slice[i] = 1;
            }

            for (int i = 0; i < 3; i++)
            {
                if (slice[i] == 1)
                {
                    if(xCount != oCount)
                    {
                        AIBoardPositive[i, j] += (int)(Mathf.Pow(3, xCount));
                        AIBoardNegative[i, j] += (int)(Mathf.Pow(3, oCount));
                    }

                }
            }
        }

        //check horizontal
        for (int i = 0; i < 3; i++)
        {
            slice = new int[3];
            xCount = 0;
            oCount = 0;
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == 'X')
                    xCount++;
                else if (board[i, j] == 'O')
                    oCount++;
                else
                    slice[i] = 1;
            }

            for (int j = 0; j < 3; j++)
            {
                if (slice[j] == 1)
                {
                    if (xCount != oCount)
                    {
                        AIBoardPositive[i, j] += (int)(Mathf.Pow(3, xCount));
                        AIBoardNegative[i, j] += (int)(Mathf.Pow(3, oCount));
                    }

                }
            }
        }

        //check left diagonal
        slice = new int[3];
        xCount = 0;
        oCount = 0;

        for (int i = 0; i < 3; i++)
        {

            if (board[i, i] == 'X')
                xCount++;
            else if (board[i, i] == 'O')
                oCount++;
            else
                slice[i] = 1;
        }

        for (int i = 0; i < 3; i++)
        {
            if (slice[i] == 1)
            {
                if (xCount != oCount)
                {
                    AIBoardPositive[i, i] += (int)(Mathf.Pow(3, xCount));
                    AIBoardNegative[i, i] += (int)(Mathf.Pow(3, oCount));
                }

            }
        }

        //check right diagonal
        int x = 0;
        slice = new int[3];
        xCount = 0;
        oCount = 0;
        for (int i = 2; i >= 0; i--)
        {
            if (board[i, x] == 'X')
                xCount++;
            else if (board[i, x] == 'O')
                oCount++;
            else
                slice[i] = 1;
            x++;
        }

        x = 0;
        for (int i = 2; i >= 0; i--)
        {
            if (slice[i] == 1)
            {
                if (xCount != oCount)
                {
                    AIBoardPositive[i, x] += (int)(Mathf.Pow(3, xCount));
                    AIBoardNegative[i, x] += (int)(Mathf.Pow(3, oCount));
                }

            }
            x++;
        }

        print($"Negative: {AIBoardNegative.ToString()}");
        print($"AIBoardPositive: {AIBoardPositive.ToString()}");

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (AIBoardPositive[i, j] >= 9)
                {
                    return new Vector2(j, i);
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (AIBoardNegative[i, j] >= 9)
                {
                    return new Vector2(j, i);
                }
            }
        }

        return moveRoutine();
    }

    public Vector2 moveRoutine()
    {
        if (currentMove == 0)
            setMoves();

        Vector2 move = xMoves[currentMove];
        currentMove++;
        return move;
    }

    public void setMoves()
    {
        xMoves.Add(new Vector2(0, 2));
    }

    public Vector2 checkAndAdd(int i, int j) 
    {
        Vector2 v = Vector2.zero;
        if (board[i, j] == 'X')
        {
            v.x = 1;
        }
        else if (board[i,j] == 'O')
        {
            v.y = 1;
        }

        return v;
    }
}
