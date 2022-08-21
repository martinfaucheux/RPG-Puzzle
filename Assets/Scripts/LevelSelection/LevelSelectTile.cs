using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelectTile : ActivableObject
{
    [SerializeField] LevelMetaData _levelData;
    [SerializeField] TextMeshPro _textComponent;
    [SerializeField] Color _lockedColor;
    [SerializeField] Color _availableColor;
    [SerializeField] Color _gemsCollectedColor;
    [SerializeField] Color _questsCompletedColor;

    // TODO: use events to update levelObjectiveList
    private LevelObjectiveList _levelObjectiveList;

    public void Initialize(LevelMetaData levelData, LevelObjectiveList levelObjectiveList)
    {
        _levelData = levelData;
        _textComponent.text = _levelData.sceneBuildIndex.ToString();
        _levelObjectiveList = levelObjectiveList;
    }

    public override IEnumerator OnEnter(GameObject sourceObject)
    {
        _levelObjectiveList.SetLevel(_levelData.sceneBuildIndex);
        yield return null;
    }
}
