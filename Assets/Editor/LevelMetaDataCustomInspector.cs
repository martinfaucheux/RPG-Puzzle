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

        if (GUILayout.Button("Bake build indices"))
        {
            BakeBuildIndices();
        }
    }

    public void BakeBuildIndices()
    {

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
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

}