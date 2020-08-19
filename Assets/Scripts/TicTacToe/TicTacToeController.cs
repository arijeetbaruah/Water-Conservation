using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeController : MonoBehaviour
{
    public List<TicTacToeButton> buttonList;
    public static TicTacToeController manager;

    // Start is called before the first frame update
    void Start()
    {
        if (manager == null)
        {
            manager = this;
            AIController();
        }
        else
        {
            Destroy(this);
        }
    }

    int checkWin()
    {
        TicTacToeButtonState side = TicTacToeButtonState.cross;
        int ret = -1;

        for (int i = -1; side != TicTacToeButtonState.circle; side = TicTacToeButtonState.circle, ret = 1)
        {
            if (buttonList[0].state == side && buttonList[1].state == side && buttonList[2].state == side)
            {
                return ret;
            }

            if (buttonList[3].state == side && buttonList[4].state == side && buttonList[5].state == side)
            {
                return ret;
            }

            if (buttonList[6].state == side && buttonList[7].state == side && buttonList[8].state == side)
            {
                return ret;
            }

            if (buttonList[0].state == side && buttonList[3].state == side && buttonList[6].state == side)
            {
                return ret;
            }

            if (buttonList[1].state == side && buttonList[4].state == side && buttonList[7].state == side)
            {
                return ret;
            }

            if (buttonList[2].state == side && buttonList[5].state == side && buttonList[8].state == side)
            {
                return ret;
            }

            if (buttonList[0].state == side && buttonList[4].state == side && buttonList[8].state == side)
            {
                return ret;
            }

            if (buttonList[2].state == side && buttonList[4].state == side && buttonList[6].state == side)
            {
                return ret;
            }
        }

        return 0;
    }

    public void EndTurn()
    {
        if (checkWin() != 0)
        {
            GameOver();
        }

        AIController();

    }

    void GameOver()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }

    public List<TicTacToeButtonState> getBoard()
    {
        List<TicTacToeButtonState> board = new List<TicTacToeButtonState>();

        for (int i = 0; i < 9; i++)
        {
            board.Add(buttonList[i].state);
        }

        return board;
    }

    public void AIController()
    {
        int bestScore = -999, bestMove = 0;
        for (int i = 0; i < 9; i++)
        {
            if (buttonList[i].state == TicTacToeButtonState.none)
            {
                buttonList[i].state = TicTacToeButtonState.circle;
                int score = minimax(getBoard());
                buttonList[i].state = TicTacToeButtonState.none;
                if (score > bestScore)
                {
                    bestMove = i;
                    bestScore = score;
                }
            }
        }
        buttonList[bestMove].state = TicTacToeButtonState.circle;
        buttonList[bestMove].RenderText();
    }

    int checkWinBoard(List<TicTacToeButtonState> board)
    {
        TicTacToeButtonState side = TicTacToeButtonState.cross;
        int ret = -99;

        for (int i = -1; side != TicTacToeButtonState.circle; side = TicTacToeButtonState.circle, ret = 1)
        {
            if (board[0] == side && board[1] == side && board[2] == side)
            {
                return ret;
            }

            if (board[3] == side && board[4] == side && board[5] == side)
            {
                return ret;
            }

            if (board[6] == side && board[7] == side && board[8] == side)
            {
                return ret;
            }

            if (board[0] == side && board[3] == side && board[6] == side)
            {
                return ret;
            }

            if (board[1] == side && board[4] == side && board[7] == side)
            {
                return ret;
            }

            if (board[2] == side && board[5] == side && board[8] == side)
            {
                return ret;
            }

            if (board[0] == side && board[4] == side && board[8] == side)
            {
                return ret;
            }

            if (board[2] == side && board[4] == side && board[6] == side)
            {
                return ret;
            }
        }

        return ret;
    }

    int minimax(List<TicTacToeButtonState> board, int depth = 0, bool isMaximiszing = false)
    {
        int result = checkWinBoard(board);
        if (result != -99)
        {
            return result;
        }

        if (isMaximiszing)
        {
            int bestScore = -99;
            for (int i = 0; i < 9; i++)
            {
                board[i] = TicTacToeButtonState.circle;
                int score = minimax(board, depth + 1, false);
                board[i] = TicTacToeButtonState.none;
                bestScore = Mathf.Max(bestScore, score);
            }
            return bestScore;
        } else
        {
            int bestScore = 99;
            for (int i = 0; i < 9; i++)
            {
                board[i] = TicTacToeButtonState.circle;
                int score = minimax(board, depth + 1, true);
                board[i] = TicTacToeButtonState.none;
                bestScore = Mathf.Min(bestScore, score);
            }
            return bestScore;
        }
        return 1;
    }
}
