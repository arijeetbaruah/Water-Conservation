﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoorInteractable : Interactable
{
    public override void ShowOptions()
    {
        List<string> interactions = new List<string>();

        interactions.Add("Start Interaction");

        GameManager.manager.ListInteraction(interactions);
    }

    public override void Interact(KeyCode key)
    {
        if (key == KeyCode.F)
        {
            QuestStatus qs;
            qs = (QuestStatus)LevelManager.manager.quests["JB"];
            qs.Complete();
            LevelManager.manager.quests["JB"] = qs;
            StartMiniGameLevel();
        }
    }
}
