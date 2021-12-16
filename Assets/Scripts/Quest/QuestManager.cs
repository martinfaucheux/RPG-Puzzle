using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest[] quests;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Quest quest in quests)
        {
            quest.Initialize();
        }

        GameEvents.instance.onEndOfLevel += CheckCompletion;
    }

    void OnDestroy()
    {
        GameEvents.instance.onEndOfLevel -= CheckCompletion;
    }

    public void CheckCompletion()
    {
        foreach (Quest quest in quests)
        {
            bool isComplete = quest.CheckCompletion();
            string str = "Quest '{0}' completed: {1}";
            Debug.Log(string.Format(str, quest.name, isComplete));
        }
    }
}
