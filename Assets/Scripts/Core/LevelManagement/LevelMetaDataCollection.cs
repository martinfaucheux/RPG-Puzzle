using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelMetaDataCollection", menuName = "Custom Objects / Levels / LevelMetaDataCollection")]
public class LevelMetaDataCollection : ScriptableObject
{
    public List<LevelMetaData> levelList;

    public LevelMetaData GetLevelBySceneBuildIndex(int sceneBuildIndex)
    {
        foreach (LevelMetaData levelMetaData in levelList)
        {
            if (levelMetaData.sceneBuildIndex == sceneBuildIndex)
            {
                return levelMetaData;
            }
        }
        Debug.LogError("Level with build index does not exist: " + sceneBuildIndex);
        return null;
    }

    public LevelMetaData GetLevelByName(string name)
    {
        foreach (LevelMetaData levelMetaData in levelList)
        {
            if (levelMetaData.name == name)
            {
                return levelMetaData;
            }
        }
        Debug.LogError("Level with name does not exist: " + name);
        return null;
    }
}
