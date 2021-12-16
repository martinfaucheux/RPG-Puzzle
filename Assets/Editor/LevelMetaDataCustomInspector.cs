using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Linq;

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

}