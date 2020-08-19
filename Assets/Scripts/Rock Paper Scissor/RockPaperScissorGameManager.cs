using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum RockPaperScissorOption
{
    Rock,
    Paper,
    Scissor
}

public class RockPaperScissorGameManager : MonoBehaviour
{
    [System.Serializable]
    public struct RockPaperScissorImage
    {
        public Sprite Rock;
        public Sprite Paper;
        public Sprite Scissor;
    };

    [System.Serializable]
    public struct VictoryStatsColor
    {
        public Color Win;
        public Color Draw;
        public Color Lost;
    };

    public enum RoundStats
    {
        inactive,
        active,
        win,
        lose,
        draw
    };

    public RockPaperScissorOption playerChoice;
    public RockPaperScissorOption enermyChoice;

    public Image playerImages;
    public Image AIImages;

    public Sprite defaultImage;

    public RockPaperScissorImage playerOptionImages;
    public RockPaperScissorImage AIOptionImages;

    public TextMeshProUGUI victoryText;

    public GameObject playerVictoryStatsLight;
    public GameObject comVictoryStatsLight;

    public RoundStats[] playerStats;
    public RoundStats[] comStats;

    public VictoryStatsColor statsColor;

    private int currentLevel = 0;

    private void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        playerStats = new RoundStats[3];
        comStats = new RoundStats[3];

        ResetGame();
    }

    public void ResetGame()
    {
        playerImages.sprite = defaultImage;
        AIImages.sprite = defaultImage;

        victoryText.SetText("");
    }

    public void AIChoice()
    {
        int option = Random.Range(0, 3);
        enermyChoice = (RockPaperScissorOption)option;

        switch (enermyChoice)
        {
            case RockPaperScissorOption.Rock:
                AIImages.sprite = playerOptionImages.Rock;
                break;
            case RockPaperScissorOption.Paper:
                AIImages.sprite = playerOptionImages.Paper;
                break;
            case RockPaperScissorOption.Scissor:
                AIImages.sprite = playerOptionImages.Scissor;
                break;
        }
    }

    IEnumerator ResetGameAtEnd()
    {
        yield return new WaitForSeconds(5);
        ResetGame();

        EnableAllOptions();
    }

    void DisableAllOptions()
    {
        RockPapaerScissorOption[] options = FindObjectsOfType<RockPapaerScissorOption>();

        foreach(RockPapaerScissorOption op in options)
        {
            op.IsClickable = false;
        }
    }

    void EnableAllOptions()
    {
        RockPapaerScissorOption[] options = FindObjectsOfType<RockPapaerScissorOption>();

        foreach (RockPapaerScissorOption op in options)
        {
            op.IsClickable = true;
        }
    }

    public void UpdatePlayerChoice(RockPaperScissorOption playerChoice)
    {
        DisableAllOptions();

        AIChoice();
        this.playerChoice = playerChoice;

        switch(playerChoice)
        {
            case RockPaperScissorOption.Rock:
                playerImages.sprite = playerOptionImages.Rock;
                break;
            case RockPaperScissorOption.Paper:
                playerImages.sprite = playerOptionImages.Paper;
                break;
            case RockPaperScissorOption.Scissor:
                playerImages.sprite = playerOptionImages.Scissor;
                break;
        }

        CalculateWinLost();
        StartCoroutine(ResetGameAtEnd());
    }

    void CalculateWinLost()
    {
        Image playerStatsLight = playerVictoryStatsLight.transform.GetChild(currentLevel).GetComponent<Image>();
        Image comStatsLight = playerVictoryStatsLight.transform.GetChild(currentLevel).GetComponent<Image>();
        
        if (playerChoice == RockPaperScissorOption.Rock)
        {
            if (enermyChoice == RockPaperScissorOption.Rock)
            {
                victoryText.SetText("Tie");
                playerStats[currentLevel] = RoundStats.draw;
                comStats[currentLevel] = RoundStats.draw;

                playerStatsLight.color = statsColor.Draw;
                comStatsLight.color = statsColor.Draw;
            } else if (enermyChoice == RockPaperScissorOption.Paper)
            {
                victoryText.SetText("Player Wins");
                playerStats[currentLevel] = RoundStats.win;
                comStats[currentLevel] = RoundStats.lose;

                playerStatsLight.color = statsColor.Win;
                comStatsLight.color = statsColor.Lost;
            }
            else
            {
                victoryText.SetText("Computer Wins");
                playerStats[currentLevel] = RoundStats.lose;
                comStats[currentLevel] = RoundStats.win;

                playerStatsLight.color = statsColor.Lost;
                comStatsLight.color = statsColor.Win;
            }
        } else if (playerChoice == RockPaperScissorOption.Paper)
        {
            if (enermyChoice == RockPaperScissorOption.Paper)
            {
                victoryText.SetText("Tie");
                playerStats[currentLevel] = RoundStats.draw;
                comStats[currentLevel] = RoundStats.draw;

                playerStatsLight.color = statsColor.Draw;
                comStatsLight.color = statsColor.Draw;
            }
            else if (enermyChoice == RockPaperScissorOption.Rock)
            {
                victoryText.SetText("Player Wins");
                playerStats[currentLevel] = RoundStats.win;
                comStats[currentLevel] = RoundStats.lose;

                playerStatsLight.color = statsColor.Win;
                comStatsLight.color = statsColor.Lost;
            }
            else
            {
                victoryText.SetText("Computer Wins");
                playerStats[currentLevel] = RoundStats.lose;
                comStats[currentLevel] = RoundStats.win;

                playerStatsLight.color = statsColor.Lost;
                comStatsLight.color = statsColor.Win;
            }
        } else
        {
            if (enermyChoice == RockPaperScissorOption.Scissor)
            {
                victoryText.SetText("Tie");
                playerStats[currentLevel] = RoundStats.draw;
                comStats[currentLevel] = RoundStats.draw;

                playerStatsLight.color = statsColor.Draw;
                comStatsLight.color = statsColor.Draw;
            }
            else if (enermyChoice == RockPaperScissorOption.Paper)
            {
                victoryText.SetText("Player Wins");
                playerStats[currentLevel] = RoundStats.win;
                comStats[currentLevel] = RoundStats.lose;

                playerStatsLight.color = statsColor.Win;
                comStatsLight.color = statsColor.Lost;
            }
            else
            {
                victoryText.SetText("Computer Wins");
                playerStats[currentLevel] = RoundStats.lose;
                comStats[currentLevel] = RoundStats.win;

                playerStatsLight.color = statsColor.Lost;
                comStatsLight.color = statsColor.Win;
            }
        }

        currentLevel++;
    }

}
