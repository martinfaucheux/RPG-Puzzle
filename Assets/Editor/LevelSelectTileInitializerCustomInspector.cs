using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(LevelSelectTileInitializer))]
public class LevelSelectTileInitializerCustomInspector : Editor
{
    private LevelSelectTileInitializer t;

    private void OnEnable()
    {
        t = target as LevelSelectTileInitializer;
    }


    public override void OnInspectorGUI()
    {
        // Show default inspector property editor
        DrawDefaultInspector();

        if (GUILayout.Button("Instantiate Tiles"))
        {
            InstantiateTiles();

            EditorUtility.SetDirty(t);
            EditorSceneManager.MarkSceneDirty(t.gameObject.scene);
        }
    }

    private void InstantiateTiles()
    {
        foreach (Transform _transform in t.transform)
            DestroyImmediate(_transform);

        t.InstantiateTiles();
    }
}