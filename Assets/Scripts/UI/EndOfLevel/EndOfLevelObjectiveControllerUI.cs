using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevelObjectiveControllerUI : MonoBehaviour
{
    [SerializeField] GameObject goToSetActive;
    [SerializeField] LevelObjectiveList levelObjectiveList;

    void Start()
    {
        GameEvents.instance.onEnterState += OnEnterState;
    }

    void OnDestroy()
    {
        GameEvents.instance.onEnterState -= OnEnterState;
    }

    private void OnEnterState(GameState state)
    {
        if (state == GameState.WIN)
        {
            Show();
        }
    }

    private void Show()
    {
        StartCoroutine(ShowCoroutine());
    }

    private IEnumerator ShowCoroutine()
    {
        yield return new WaitForEndOfFrame();
        int levelId = LevelLoader.instance.currentLevelId;
        goToSetActive.SetActive(true);
        levelObjectiveList.SetLevel(levelId);
    }
}
