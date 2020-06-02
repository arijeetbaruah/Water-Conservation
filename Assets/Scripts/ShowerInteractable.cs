using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerInteractable : Interactable
{
    public override void Interact(KeyCode key)
    {
        if (key == KeyCode.F)
        {
            LevelManager.manager.UseWater(5f);
        }
    }

    public override void ShowOptions()
    {
        List<string> interactions = new List<string>();

        interactions.Add("Take Shower");

        GameManager.manager.ListInteraction(interactions);
    }
}
