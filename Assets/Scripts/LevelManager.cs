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
            LevelManager.manager.AddQuest(q.questName, q);
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

    private void Start()
    {
        if (!GameManager.manager.hintHandler.gameObject.activeSelf)
            AddQuest(startQuest.questID, startQuest);
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
                txt.SetText(q.quest.questName);
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
