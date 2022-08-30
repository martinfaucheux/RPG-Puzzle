using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : SingletoneBase<SaveManager>
{
    [SerializeField] PlayerData _playerData;

    public int lastPlayedLevel { get => _playerData.lastPlayedLevel; }

    protected override void Awake()
    {
        base.Awake();
        Load();
    }

    public LevelProgressData GetLevelData(int levelId) => _playerData[levelId];

    public void SaveLevelProgress(int levelId, LevelProgressData levelData)
    {
        _playerData[levelId] = levelData;
        Save();
    }

    public void SaveLastLevelPlayed(int levelId)
    {
        // Check that it corresponds to a right level id
        if (
            LevelLoader.IsLevelId(levelId)
            && _playerData.lastPlayedLevel != levelId
        )
        {
            _playerData.SetLastPlayedLevel(levelId);
            Save();
        }
    }
    public void UnlockLevel(int levelId)
    {
        _playerData.Unlock(levelId);
        Save();
    }
    public void UnlockLevels(List<int> levelIds)
    {
        foreach (int levelId in levelIds)
            _playerData.Unlock(levelId);
        Save();
    }

    // forward methods to PlayerData
    public bool IsLevelUnlocked(int levelId) => _playerData.IsUnlocked(levelId);
    public bool IsGemCollected(int levelId, int gemId) => _playerData.IsGemCollected(levelId, gemId);
    public bool IsQuestCompleted(int levelId, int questId) => _playerData.IsQuestCompleted(levelId, questId);
    public List<int> GetUnlockedLevels() => _playerData.GetUnlockedLevels();
    public int GetCollectedGemCount(int levelId) => _playerData.GetCollectedGemCount(levelId);
    public int GetCompletedQuestsCount(int levelId) => _playerData.GetCompletedQuestsCount(levelId);

    // forward methods to DataSaver
    public void DeleteSavedData() => DataSaver.DeleteSavedData();
    private void Save() => DataSaver.SaveGameState(_playerData);
    private void Load() => _playerData = DataSaver.LoadGameState();

}
