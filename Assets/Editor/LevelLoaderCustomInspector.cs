using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(LevelLoader))]
public class LevelLoaderCustomInspector : Editor
{
    private LevelLoader t;

    private void OnEnable() {
        t = target as LevelLoader;
        SceneView.duringSceneGui -= OnScene;
        SceneView.duringSceneGui += OnScene;
    }

    void OnScene(SceneView sceneview)
    {
        // if (t.showSceneBounds)
        // {
        //     Vector2 origin = t.origin - Vector2.one * 0.5f;
        //     float matrixWidth = t.matrixSize.x;
        //     float matrixHeight = t.matrixSize.y;

        //     Vector3[] verts = new Vector3[]
        //     {
        //     new Vector3(origin.x, origin.y),
        //     new Vector3(origin.x, origin.y + matrixHeight),
        //     new Vector3(origin.x + matrixWidth, origin.y + matrixHeight),
        //     new Vector3(origin.x + matrixWidth, origin.y),
        //     };

        //     Handles.DrawSolidRectangleWithOutline(verts, t.sceneBoundsColor, new Color(0, 0, 0, 1));
        // }
    }

    public override void OnInspectorGUI()
    {
        // Show default inspector property editor
        DrawDefaultInspector();

        // if (GUILayout.Button("Load Saved State"))
        // {
        //     t.LoadLastLevelPlayed();
        // }

        if (GUILayout.Button("Delete Saved State"))
        {
            t.DeleteSavedData();
        }

        // if (GUI.changed)
        // {
        //     EditorUtility.SetDirty(t);
        //     EditorSceneManager.MarkSceneDirty(t.gameObject.scene);
        // }
    }
}