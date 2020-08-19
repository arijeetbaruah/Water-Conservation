using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGameCard : MonoBehaviour
{
    public string cardID;
    public Sprite targetImage;
    private Sprite defaultImage;
    private Image image;

    public bool isOpen = false;

    public void Awake()
    {
        image = GetComponent<Image>();
        defaultImage = image.sprite;
    }

    IEnumerator CloseCard()
    {
        yield return new WaitForSeconds(2);

        MemoryGame.game.selectedCard.isOpen = false;
        MemoryGame.game.selectedCard.GetComponent<Image>().sprite = defaultImage;
        MemoryGame.game.selectedCard = null;

        isOpen = false;
        image.sprite = defaultImage;
        MemoryGame.game.allowCardSelection = true;
    }

    IEnumerator MatchCard()
    {
        yield return new WaitForSeconds(1);

        MemoryGameCard temp = MemoryGame.game.selectedCard;
        MemoryGame.game.selectedCard = null;
        MemoryGame.game.GetComponent<AudioSource>().Play();

        Destroy(temp.gameObject);
        Destroy(gameObject);
        MemoryGame.game.allowCardSelection = true;
    }

    public void ClickAction()
    {
        if (isOpen || !MemoryGame.game.allowCardSelection)
        {
            return;
        }
        if (MemoryGame.game.selectedCard == null)
        {
            image.sprite = targetImage;
            MemoryGame.game.selectedCard = this;
            isOpen = true;
        }
        else
        {
            if (MemoryGame.game.selectedCard.cardID == cardID)
            {
                image.sprite = targetImage;
                MemoryGame.game.allowCardSelection = false;
                StartCoroutine(MatchCard());
            }
            else
            {
                image.sprite = targetImage;
                MemoryGame.game.allowCardSelection = false;

                StartCoroutine(CloseCard());
            }
        }
    }
}
