using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum TicTacToeButtonState
{
    none,
    cross,
    circle
}

public class TicTacToeButton : MonoBehaviour
{
    public TicTacToeButtonState state;
    TextMeshProUGUI buttonTxt;

    public void Awake()
    {
        buttonTxt = GetComponentInChildren<TextMeshProUGUI>();
        state = TicTacToeButtonState.none;
        RenderText();
    }

    public void OnClick()
    {
        state = TicTacToeButtonState.cross;
        RenderText();
        TicTacToeController.manager.EndTurn();
        GetComponent<UnityEngine.UI.Button>().interactable = false;

    }

    public void RenderText()
    {
        switch (state)
        {
            case TicTacToeButtonState.none:
                buttonTxt.SetText(" ");
                break;
            case TicTacToeButtonState.cross:
                buttonTxt.SetText("X");
                break;
            case TicTacToeButtonState.circle:
                buttonTxt.SetText("O");
                break;
        }
    }
}
