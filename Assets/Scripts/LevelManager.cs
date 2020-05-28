using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Analytics;

public class QuestStatus
{
    public Quest quest;
    public bool isComplete;

    public void Complete()
    {
        isComplete = true;
        foreach(Quest q in quest.nextQuests)
        {
            q.questTrackNumberComplete = 0;
            LevelManager.manager.AddQuest(q.questID, q);
        }

        AnalyticsEvent.Custom(quest.name, new Dictionary<string, object> { { "time_elapsed", Time.timeSinceLevelLoad } });
        LevelManager.manager.UpdateQuest();
    }
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager manager;

    public RectTransform questHolder;

    public Hashtable quests = new Hashtable();
    public Quest startQuest;

    public float questInfoWaitTime = 5;

    public Slider waterLeftSlider;

    public float waterLeft;
    float maxWaterLeft;

    public float MaxWaterLeft { get => maxWaterLeft; }

    public void Awake()
    {
        if (manager == null)
        {
            manager = this;
            maxWaterLeft = waterLeft;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        if (waterLeftSlider)
        {
            waterLeftSlider.maxValue = waterLeft;
            waterLeftSlider.value = waterLeft;

            waterLeftSlider.minValue = 0;
        }
    }

    private void Start()
    {
        if (!GameManager.manager.gameOver)
            if (!GameManager.manager.hintHandler.gameObject.activeSelf)
                AddQuest(startQuest.questID, startQuest);
    }

    public bool UseWater(float water)
    {
        if (waterLeft >= water)
        {
            waterLeft -= water;
            waterLeftSlider.value = waterLeft;

            return true;
        }

        return false;
    }

    public void UpdateQuest()
    {

        for(int i = 0; i < questHolder.childCount; i++)
        {
            Transform qh = questHolder.GetChild(i);
            Destroy(qh.gameObject);
        }

        string[] keys = new string[quests.Keys.Count];
        quests.Keys.CopyTo(keys, 0);

        foreach (string k in keys)
        {
            QuestStatus q = (QuestStatus)quests[k];
            if (!q.isComplete)
            {
                TextMeshProUGUI txt = Instantiate<TextMeshProUGUI>(GameManager.manager.textPrefab, questHolder.transform);
                if (q.quest.questTrackNumber == 1)
                    txt.SetText(q.quest.questName);
                else
                    txt.SetText(q.quest.questName + "("+ 0 + "/" + q.quest.questTrackNumber +")");
            }
        }
    }

    IEnumerator ShowQuestInScreen(Quest quest)
    {
        TextMeshProUGUI txt = Instantiate<TextMeshProUGUI>(GameManager.manager.textPrefab, GameManager.manager.interactableTxtHolder);
        txt.SetText(quest.questName);
        yield return new WaitForSeconds(questInfoWaitTime);

        Destroy(txt.gameObject);
    }

    public void AddQuest(string ID, Quest quest)
    {
        QuestStatus qs = new QuestStatus();
        qs.quest = quest;
        qs.isComplete = false;

        StartCoroutine(ShowQuestInScreen(quest));
        quests.Add(ID, qs);
        UpdateQuest();
    }
}
