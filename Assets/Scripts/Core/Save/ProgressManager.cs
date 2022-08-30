using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// <summary>
// track the progress of quests completed and gems collected within a level
// </summary>
public class ProgressManager : SingletoneBase<ProgressManager>
{

    [SerializeField] LevelProgressData _progressData;

    public int currentLevelId { get => LevelLoader.instance.currentLevelId; }

    void Start()
    {
        _progressData = FetchFromSave();
    }

    public void AddGem(int gemId) => _progressData.AddGem(gemId);
    public void AddQuest(int gemId) => _progressData.AddQuest(gemId);

    public void Save()
    {
        // combine SaveProgress from current run with already saved data
        LevelProgressData combinedProgress = _progressData.Merge(FetchFromSave());
        SaveManager.instance.SaveLevelProgress(currentLevelId, combinedProgress);
    }

    private LevelProgressData FetchFromSave()
    {
        return SaveManager.instance.GetLevelData(currentLevelId);
    }


}
