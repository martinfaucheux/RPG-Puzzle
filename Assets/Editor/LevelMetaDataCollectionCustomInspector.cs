using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

[CustomEditor(typeof(LevelMetaDataCollection))]
public class LevelMetaDataCollectionCustomInspector : Editor
{
    private LevelMetaDataCollection t;

    private static string _levelNamePattern = @"Level\s([\d\.]+)";
    private Regex _regex;

    private void OnEnable()
    {
        t = target as LevelMetaDataCollection;
        _regex = new Regex(_levelNamePattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }


    public override void OnInspectorGUI()
    {
        // Show default inspector property editor
        DrawDefaultInspector();

        if (GUILayout.Button("Bake build indices"))
        {
            BakeBuildIndices();
        }

        if (GUILayout.Button("Update OverWorld Positions"))
        {
            UpdateOverWorldPositions();
        }
    }

    private void BakeBuildIndices()
    {
        // set build indices
        foreach (LevelMetaData levelMetaData in t.levelList)
        {
            string scenePath = string.Format(
                "Assets/Scenes/Levels/{0}.unity", levelMetaData.sceneName
            );
            int sceneBuildIndex = SceneUtility.GetBuildIndexByScenePath(scenePath);

            if (sceneBuildIndex < 0)
            {
                Debug.LogError(string.Format("Scene not found: '{0}'", levelMetaData.sceneName));
            }
            else
            {
                levelMetaData.sceneBuildIndex = sceneBuildIndex;
                EditorUtility.SetDirty(levelMetaData);
            }
        }

        // order metadata items per build index
        t.levelList = t.levelList.OrderBy(x => x.sceneBuildIndex).ToList();
        EditorUtility.SetDirty(t);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private GenericGrid<string> ReadLevelGrid()
    {
        string filePath = Application.dataPath + t.csvFilePath;
        Debug.Log("reading csv data at: " + filePath);

        GenericGrid<string> grid = new GenericGrid<string>();
        int x = 0;
        using (var reader = new StreamReader(filePath))
        {
            while (!reader.EndOfStream)
            {
                int y = 0;
                string strLine = reader.ReadLine();
                foreach (string levelId in strLine.Split(t.csvSeparator))
                {
                    if (levelId != "")
                        grid[x, y] = ClearLevelId(levelId);
                    y++;
                }
                x++;
            }
        }
        return grid;
    }

    private void UpdateOverWorldPositions()
    {
        GenericGrid<string> stringGrid = ReadLevelGrid();
        GenericGrid<LevelMetaData> levelGrid = ConvertToLevelMetaData(stringGrid);

        foreach (KeyValuePair<Vector2Int, LevelMetaData> kvp in levelGrid)
        {
            LevelMetaData levelMetaData = kvp.Value;
            levelMetaData.overWorldPostion = kvp.Key;
            EditorUtility.SetDirty(levelMetaData);
        }
    }

    private string ClearLevelId(string levelId)
    {
        Match match = _regex.Match(levelId);
        if (match.Success)
        {
            Group group = match.Groups[1];
            return group.Value;
        }
        return levelId;
    }

    private static LevelMetaData[] GetAllLevelMetaData()
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(LevelMetaData).Name);
        LevelMetaData[] levels = new LevelMetaData[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            levels[i] = AssetDatabase.LoadAssetAtPath<LevelMetaData>(path);
        }
        return levels;
    }

    private GenericGrid<LevelMetaData> ConvertToLevelMetaData(
        GenericGrid<string> stringGrid
    )
    {
        GenericGrid<LevelMetaData> levelDataGrid = new GenericGrid<LevelMetaData>();

        LevelMetaData[] levelList = GetAllLevelMetaData();
        foreach (KeyValuePair<Vector2Int, string> kvp in stringGrid)
        {
            string levelId = kvp.Value;
            LevelMetaData levelData = null;
            foreach (LevelMetaData _levelData in levelList)
            {
                if (ClearLevelId(_levelData.sceneName) == levelId)
                {
                    levelData = _levelData;
                    levelDataGrid[kvp.Key] = levelData;
                    break;
                }
            }
            if (levelData == null)
                throw new System.Exception("Level not found: " + levelId);
        }
        return levelDataGrid;
    }
}