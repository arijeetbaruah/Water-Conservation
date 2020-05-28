using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishwasherInteractable : Interactable
{
    Player player;

    int dishLoaded = 0;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public override void ShowOptions()
    {
        List<string> interactions = new List<string>();

        if (player.dishInHand > 0)
            interactions.Add("Load Dishes");

        if (dishLoaded > 0)
            interactions.Add("Start Washing");

        GameManager.manager.ListInteraction(interactions);
    }

    public override void Interact(KeyCode key)
    {
        if (key == KeyCode.F && player.dishInHand > 0)
        {
            player.dishInHand = 0;
            dishLoaded++;
        }
        else if ((key == KeyCode.F && player.dishInHand == 0 && dishLoaded > 0) || (key == KeyCode.E && player.dishInHand > 0 && dishLoaded > 0))
        {
            LevelManager.manager.UseWater(5f);

            DishInteractable[] dishes = FindObjectsOfType<DishInteractable>();
            
            if (dishes.Length == 0)
            {
                dishLoaded = 0;
                QuestStatus qs;
                qs = (QuestStatus)LevelManager.manager.quests["WD"];
                qs.Complete();
                LevelManager.manager.quests["WD"] = qs;
            }
            else
            {
                QuestStatus qs;
                qs = (QuestStatus)LevelManager.manager.quests["WD"];
                qs.quest.questTrackNumberComplete += dishLoaded;
                LevelManager.manager.UpdateQuest();
                LevelManager.manager.quests["WD"] = qs;

                dishLoaded = 0;
            }
        }
    }
}