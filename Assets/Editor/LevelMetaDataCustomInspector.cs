using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(LevelMetaDataCollection))]
public class LevelMetaDataCollectionCustomInspector : Editor
{

    private LevelMetaDataCollection t;

    private void OnEnable()
    {
        t = target as LevelMetaDataCollection;
    }

    public override void OnInspectorGUI()
    {
        // Show default inspector property editor
        DrawDefaultInspector();

        if (GUILayout.Button("Check Integrity"))
        {
            CheckIntegrity();
        }
    }

    public void CheckIntegrity()
    {
        string prefix = "Assets/Scenes/Levels/";

        foreach (LevelMetaData levelMetaData in t.levelList)
        {
            string scenePath = prefix + levelMetaData.sceneName + ".unity";
            int sceneIndex = SceneUtility.GetBuildIndexByScenePath(scenePath);

            if (sceneIndex < 0)
            {
                Debug.LogError(string.Format("Invalid Scene name: '{0}'", levelMetaData.sceneName));
            }
        }
    }

}