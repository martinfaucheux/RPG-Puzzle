using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelectTile : ActivableObject
{

    [field: SerializeField]
    public LevelMetaData levelData { get; private set; }
    [SerializeField] TextMeshPro _textComponent;
    [SerializeField] SpriteRenderer _spriteRenderer;

    [SerializeField] Color _lockedColor;
    [SerializeField] Color _availableColor;
    [SerializeField] Color _gemsCollectedColor;
    [SerializeField] Color _questsCompletedColor;

    private bool _isUnlocked;

    protected override void Start()
    {
        base.Start();

        _isUnlocked = LevelLoader.instance.IsLevelUnlocked(levelData.sceneBuildIndex);
        _spriteRenderer.color = GetColor();
    }

    public void SetLevelMetaData(LevelMetaData levelData)
    {
        this.levelData = levelData;
        _textComponent.text = levelData.sceneBuildIndex.ToString();
    }

    public override bool CheckAllowInteraction(GameObject sourceObject) => _isUnlocked;

    public override IEnumerator OnEnter(GameObject sourceObject)
    {
        SelectLevel();
        yield return null;
    }

    public void SelectLevel() => LevelSelectManager.instance.SelectLevel(levelData.sceneBuildIndex);


    private Color GetColor()
    {
        int levelId = levelData.sceneBuildIndex;
        PlayerData playerData = LevelLoader.instance.playerSavedData;

        bool isUnlocked = playerData.IsUnlocked(levelId);
        int collectedGems = playerData.GetCollectedGemCount(levelId);
        int completedQuests = playerData.GetCompletedQuestsCount(levelId);

        if (!isUnlocked)
            return _lockedColor;

        if (collectedGems == levelData.gemCount)
        {
            if (completedQuests == levelData.quests.Count)
                return _questsCompletedColor;
            else
                return _gemsCollectedColor;
        }
        else
            return _availableColor;
    }
}