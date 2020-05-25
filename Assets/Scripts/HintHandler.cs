using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintHandler : MonoBehaviour
{
    public List<RectTransform> hints;

    public int currentHint = 0;

    public Button previousBtn;
    public Button nextBtn;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.manager.playing = false;
        GameManager.manager.player.enabled = false;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        foreach (RectTransform hint in hints)
        {
            hint.gameObject.SetActive(false);
        }

        ShowCurrent();
    }

    void ShowCurrent()
    {
        hints[currentHint].gameObject.SetActive(true);
        if (currentHint == 0)
        {
            previousBtn.gameObject.SetActive(false);
        }
        else
        {
            previousBtn.gameObject.SetActive(true);
        }

        if (currentHint == hints.Count - 1)
        {
            nextBtn.gameObject.SetActive(false);
        }
        else
        {
            nextBtn.gameObject.SetActive(true);
        }
    }

    public void NextHint()
    {
        if (currentHint != hints.Count - 1)
        {
            hints[currentHint].gameObject.SetActive(false);
            currentHint++;

            ShowCurrent();
        }
    }

    public void PreviousHint()
    {
        if (currentHint != 0)
        {
            hints[currentHint].gameObject.SetActive(false);
            currentHint--;

            ShowCurrent();
        }
    }

    public void CloseHint()
    {
        GameManager.manager.playing = true;
        GameManager.manager.player.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        
        gameObject.SetActive(false);

        LevelManager.manager.AddQuest(LevelManager.manager.startQuest.questID, LevelManager.manager.startQuest);

    }
}
