using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Interactable : MonoBehaviour
{
    public abstract void ShowOptions();

    public int minigameLevel;

    public abstract void Interact(KeyCode key);

    protected void StartMiniGameLevel()
    {
        GameManager.manager.StartMiniGame();
        SceneManager.LoadScene(minigameLevel);
    }
}
