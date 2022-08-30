using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelMetaDataCollection", menuName = "Custom Objects / Levels / LevelMetaDataCollection")]
public class LevelMetaDataCollection : ScriptableObject
{
    public List<LevelMetaData> levelList;
    [field: SerializeField]
    [field: Tooltip("path of the csv file to read level position from. Root is Assets/.")]
    public string csvFilePath { get; private set; } = "/Resources/Levels/LevelGrid.csv";

    [field: SerializeField]
    [field: Tooltip("Character to use when parsing csv file")]
    public char csvSeparator { get; private set; } = ',';
    public LevelMetaData GetLevelBySceneBuildIndex(int sceneBuildIndex)
    {
        foreach (LevelMetaData levelMetaData in levelList)
        {
            if (levelMetaData.sceneBuildIndex == sceneBuildIndex)
            {
                return levelMetaData;
            }
        }
        // Debug.LogError("Level with build index does not exist: " + sceneBuildIndex);
        return null;
    }

    public GenericGrid<LevelMetaData> GetLevelGrid()
    {
        GenericGrid<LevelMetaData> levelGrid = new GenericGrid<LevelMetaData>();
        foreach (LevelMetaData levelData in levelList)
        {
            levelGrid[levelData.overWorldPostion] = levelData;
        }
        return levelGrid;
    }
}
