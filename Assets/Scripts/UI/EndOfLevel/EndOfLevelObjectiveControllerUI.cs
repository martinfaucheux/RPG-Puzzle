using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevelObjectiveControllerUI : MonoBehaviour
{
    [SerializeField] GameObject goToSetActive;
    [SerializeField] LevelObjectiveList levelObjectiveList;

    void Start()
    {
        GameEvents.instance.onEndOfLevel += Show;
    }

    void OnDestroy()
    {
        GameEvents.instance.onEndOfLevel -= Show;
    }

    private void Show()
    {
        int levelId = LevelLoader.instance.currentLevelId;
        goToSetActive.SetActive(true);
        levelObjectiveList.SetLevel(levelId);
    }
}
