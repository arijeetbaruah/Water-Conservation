using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public KeyCode[] interactionKeys;

    public static GameManager manager;

    public RectTransform pauseMenu;

    private List<GameObject> interactionTxt = new List<GameObject>();

    public TextMeshProUGUI textPrefab;
    public RectTransform interactableTxtHolder;

    public bool gameOver = false;

    public bool playing = true;
    public FirstPersonAIO player;
    public RectTransform questHolder;
    public Slider waterLeftSlider;

    public HintHandler hintHandler;

    public float timer = 0;
    public float bonus = 0;

    public void ClearInteraction()
    {
        foreach (GameObject txt in interactionTxt)
        {
            Destroy(txt);
        }
    }

    public void ListInteraction(List<string> interaction)
    {
        ClearInteraction();

        for (int i = 0; i < interaction.Count; i++)
        {
            TextMeshProUGUI txt = Instantiate<TextMeshProUGUI>(textPrefab, interactableTxtHolder.transform);
            txt.SetText("Press '" + interactionKeys[i].ToString() + "' to " + interaction[i]);
            interactionTxt.Add(txt.gameObject);
        }
    }

    public void Awake()
    {
        if (manager == null)
        {
            manager = this;
            timer = 0;
        }
        else
        {
            Destroy(this);
        }

        if (MenuManagement.manager)
            hintHandler.gameObject.SetActive(MenuManagement.manager.tutorialToggle.isOn);

        if(InterGameData.data)
            if (InterGameData.data.playerPos != null)
            {
                player.transform.position = InterGameData.data.playerPos;
                player.transform.rotation = InterGameData.data.playerRot;
            }
    }

    public void ResumeGame()
    {
        playing = true;
        player.enabled = true;
        pauseMenu.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartMiniGame()
    {
        InterGameData.data.playerPos = player.transform.position;
        InterGameData.data.playerRot = player.transform.rotation;
    }

    public void Update()
    {
        if (!gameOver && playing)
        {
            timer += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            playing = false;
            player.enabled = false;
            pauseMenu.gameObject.SetActive(true);

            Cursor.lockState = CursorLockMode.None;

        }   
    }
}
