using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkInteractable : Interactable
{
    public ParticleSystem system;
    public bool isWaterRunning;

    void Start()
    {
        isWaterRunning = system.isPlaying;
    }

    public override void ShowOptions() {
        List<string> interactions = new List<string>();

        if (isWaterRunning)
        {
            interactions.Add("With Water");
            interactions.Add("Without Water");
        }
        else
            interactions.Add("Brush");

        GameManager.manager.ListInteraction(interactions);
    }

    public override void Interact(KeyCode key)
    {
        isWaterRunning = !isWaterRunning;
        if (isWaterRunning)
        {
            system.Play();
        }
        else
        {
            QuestStatus qs;
            qs = (QuestStatus)LevelManager.manager.quests["BYT"];
            qs.Complete();
            LevelManager.manager.quests["BYT"] = qs;

            //switch (key)
            //{
            //    case KeyCode.F:
            //        break;
            //    case KeyCode.E:
            //        qs.isComplete = true; 
            //        break;
            //}
            system.Stop();
        }
    }
}
