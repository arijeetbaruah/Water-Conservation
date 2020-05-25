using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum MenuState{
    MAIN,
    OPTION
};

public class MenuManagement : MonoBehaviour
{

    public static MenuManagement manager;

    MenuState state = MenuState.MAIN;

    public RectTransform mainMenu;
    public RectTransform optionMenu;

    public Toggle tutorialToggle;

    public void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void Start()
    {
        LoadState();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowOption()
    {
        state = MenuState.OPTION;
        optionMenu.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }

    public void ReturnToMain()
    {
        state = MenuState.MAIN;
        optionMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public void LoadState()
    {
        tutorialToggle.isOn = PlayerPrefs.GetInt("tutorial") == 0;
    }

    public void ToggleTutorial()
    {
        bool toggleState = tutorialToggle.isOn;
        PlayerPrefs.SetInt("tutorial", (toggleState? 0: 1));
    }
}
