using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Quest", menuName = "quest", order =1)]
public class Quest : ScriptableObject
{
    public string questID;
    public string questName;
    public int questTrackNumber = 1;

    public Quest[] nextQuests;
}
