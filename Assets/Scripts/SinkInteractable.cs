using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SinkInteractable : Interactable
{
    public ParticleSystem system;
    public bool isWaterRunning;
    Player player;
    public CinemachineBrain cinemachineBrain;
    public GameObject playerModel;

    void Start()
    {
        isWaterRunning = system.isPlaying;
        player = FindObjectOfType<Player>();
    }

    public override void ShowOptions() {
        List<string> interactions = new List<string>();

        if (isWaterRunning)
        {
            interactions.Add("With Water");
            interactions.Add("Without Water");

            cinemachineBrain.gameObject.SetActive(true);
            playerModel.SetActive(true);
            player.GetComponent<FirstPersonAIO>().enabled = false;
            player.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            interactions.Add("Brush");
        
            cinemachineBrain.gameObject.SetActive(false);
            playerModel.SetActive(false);
            player.GetComponent<FirstPersonAIO>().enabled = true;
            player.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }

        GameManager.manager.ListInteraction(interactions);
    }

    public override void Interact(KeyCode key)
    {
        isWaterRunning = !isWaterRunning;
        if (isWaterRunning && LevelManager.manager.waterLeft >= 5f)
        {
            system.Play();
        } else if (LevelManager.manager.waterLeft < 5f)
        {
            Debug.Log("Out of water");
        }
        else
        {
            QuestStatus qs;
            qs = (QuestStatus)LevelManager.manager.quests["BYT"];
            qs.Complete();
            LevelManager.manager.quests["BYT"] = qs;

            switch (key)
            {
                case KeyCode.F:
                    LevelManager.manager.UseWater(10f);
                    break;
                case KeyCode.E:
                    LevelManager.manager.UseWater(5f);
                    qs.isComplete = true;
                    StartMiniGameLevel();
                    break;
            }
            system.Stop();
        }
    }
}
