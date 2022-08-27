using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(LevelLoader))]
public class LevelLoaderCustomInspector : Editor
{
    private LevelLoader t;

    private void OnEnable()
    {
        t = target as LevelLoader;
    }



    public override void OnInspectorGUI()
    {
        // Show default inspector property editor
        DrawDefaultInspector();

        // TODO: move this to SaveManager custom inspector

        if (GUILayout.Button("Delete Saved State"))
        {
            Debug.LogError("Move this to SaveManager");
            t.GetComponent<SaveManager>().DeleteSavedData();
        }

        if (GUILayout.Button("Unlock all levels"))
        {
            Debug.LogError("Move this to SaveManager");
            UnlockAllLevels();
        }


    }

    private void UnlockAllLevels()
    {
        PlayerData playerSavedData = new PlayerData();
        foreach (LevelMetaData levelMetaData in t.levelCollection.levelList)
        {
            playerSavedData.Unlock(levelMetaData.sceneBuildIndex);
        }
        DataSaver.SaveGameState(playerSavedData);
    }

    private void UnlockAllLevelsOLD()
    {
        Dictionary<int, LevelProgressData> levelDict = new Dictionary<int, LevelProgressData>();
        foreach (LevelMetaData levelMetaData in t.levelCollection.levelList)
        {
            List<bool> gemList = new List<bool>();
            for (int i = 0; i < levelMetaData.gemCount; i++)
            {
                gemList.Add(false);
            }
            bool[] gemsCollected = gemList.ToArray();

            List<bool> questList = new List<bool>();
            for (int i = 0; i < levelMetaData.quests.Count; i++)
            {
                questList.Add(false);
            }
            bool[] questsCompleted = questList.ToArray();

            levelDict[levelMetaData.sceneBuildIndex] = new LevelProgressData(gemsCollected, questsCompleted);
        }

        PlayerData playerSavedData = new PlayerData(levelDict, new Dictionary<string, bool>());
        DataSaver.SaveGameState(playerSavedData);
    }
}