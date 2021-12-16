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


        if (GUILayout.Button("Delete Saved State"))
        {
            t.DeleteSavedData();
        }

        if (GUILayout.Button("Unlock all levels"))
        {
            UnlockAllLevels();
        }


    }

    private void UnlockAllLevels()
    {
        Dictionary<int, LevelSaveData> levelDict = new Dictionary<int, LevelSaveData>();
        foreach (LevelMetaData levelMetaData in t.levelCollection.levelList)
        {
            bool[] gemsCollected = new List<bool>(levelMetaData.gemCount).ToArray();
            bool[] questsCompleted = new bool[] { false };
            levelDict[levelMetaData.sceneBuildIndex] = new LevelSaveData(gemsCollected, questsCompleted);
        }

        PlayerData playerSavedData = new PlayerData(levelDict);
        DataSaver.SaveGameState(playerSavedData);
    }
}