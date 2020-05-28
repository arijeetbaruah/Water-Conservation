using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Analytics;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI timerTxt;
    public TextMeshProUGUI waterSavedTxt;
    public TextMeshProUGUI scoreTxt;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.manager.gameOver = true;
        Cursor.visible = true;

        timerTxt.SetText(GameManager.manager.timer + "s");
        waterSavedTxt.SetText(LevelManager.manager.waterLeft.ToString());
        float score = calculationScore();
        scoreTxt.SetText(score.ToString());

        AnalyticsEvent.Custom("GameOver", new Dictionary<string, object>{
            { "score", score },
            { "time", GameManager.manager.timer },
            { "water Saved", (LevelManager.manager.waterLeft/ LevelManager.manager.MaxWaterLeft * 100) }
        });
    }

    float calculationScore()
    {
        float score = (1 / GameManager.manager.timer) + (LevelManager.manager.waterLeft/ LevelManager.manager.MaxWaterLeft);
        return Mathf.Round(score * 100);
    }

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
