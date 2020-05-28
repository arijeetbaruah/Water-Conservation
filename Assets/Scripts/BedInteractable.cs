using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedInteractable : Interactable
{
    public override void Interact(KeyCode key)
    {
        if (key == KeyCode.F)
        {
            SceneManager.LoadScene(2);
        }
    }

    public override void ShowOptions()
    {
        List<string> interactions = new List<string>();

        interactions.Add("go to sleep");

        GameManager.manager.ListInteraction(interactions);
    }
}
