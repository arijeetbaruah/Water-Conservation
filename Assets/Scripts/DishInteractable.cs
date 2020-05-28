using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishInteractable : Interactable
{
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public override void ShowOptions()
    {
        QuestStatus qs = (QuestStatus)LevelManager.manager.quests["WD"];

        if (qs != null)
        {
            List<string> interactions = new List<string>();

            interactions.Add("Take Dish");

            GameManager.manager.ListInteraction(interactions);
        }
    }

    public override void Interact(KeyCode key)
    {
        QuestStatus qs = (QuestStatus)LevelManager.manager.quests["WD"];

        if (qs != null)
        {
            if (key == KeyCode.F)
            {
                if (player.dishInHand < player.maxDishInHand)
                {
                    player.dishInHand++;
                    Destroy(transform.parent.gameObject);
                }
                else
                    Debug.Log("max out");
            }
        }
    }
}
