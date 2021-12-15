using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelMetaDataCollection", menuName = "Custom Objects / Levels / LevelMetaDataCollection")]
public class LevelMetaDataCollection : ScriptableObject
{
    public List<LevelMetaData> levelList;

    public LevelMetaData GetLevelBySlug(string slug)
    {
        foreach (LevelMetaData levelMetaData in levelList)
        {
            if (levelMetaData.slug == slug)
            {
                return levelMetaData;
            }
        }
        Debug.LogError("Level with slug does not exist: " + slug);
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
