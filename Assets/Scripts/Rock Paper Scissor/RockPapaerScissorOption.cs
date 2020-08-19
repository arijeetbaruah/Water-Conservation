using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPapaerScissorOption : MonoBehaviour
{
    private RockPaperScissorGameManager manager;

    public RockPaperScissorOption option;

    public bool IsClickable = true;

    public void Awake()
    {
        manager = FindObjectOfType<RockPaperScissorGameManager>();
    }

    public void ClickAction()
    {
        if (IsClickable)
            manager.UpdatePlayerChoice(option);
    }
}
