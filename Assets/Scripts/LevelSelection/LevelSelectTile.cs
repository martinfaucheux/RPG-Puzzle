using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectTile : ActivableObject
{
    [SerializeField] LevelMetaData _levelData;

    // TODO: use events to update levelObjectiveList
    [SerializeField] LevelObjectiveList _levelObjectiveList;

    public override IEnumerator OnEnter(GameObject sourceObject)
    {
        _levelObjectiveList.SetLevel(_levelData.sceneBuildIndex);
        yield return null;
    }

    public void AttachToPanel(LevelObjectiveList panel)
    {
        _levelObjectiveList = panel;
    }
}
