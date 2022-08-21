using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelectTile : ActivableObject
{
    [SerializeField] LevelMetaData _levelData;
    [SerializeField] TextMeshPro _textComponent;
    [SerializeField] SpriteRenderer _spriteRenderer;

    // TODO: use events to update levelObjectiveList
    private LevelObjectiveList _levelObjectiveList;
    private bool _isUnlocked;

    public void Initialize(LevelMetaData levelData, LevelObjectiveList levelObjectiveList, Color color)
    {
        _levelData = levelData;
        _textComponent.text = _levelData.sceneBuildIndex.ToString();
        _levelObjectiveList = levelObjectiveList;
        _spriteRenderer.color = color;

        _isUnlocked = LevelLoader.instance.playerSavedData.IsUnlocked(levelData.sceneBuildIndex);
    }

    public override bool CheckAllowInteraction(GameObject sourceObject)
    {
        return _isUnlocked;
    }

    public override IEnumerator OnEnter(GameObject sourceObject)
    {
        _levelObjectiveList.SetLevel(_levelData.sceneBuildIndex);
        yield return null;
    }
}
