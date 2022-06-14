using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(CollisionMatrix))]
public class CollisionMatrixCustomInspector : Editor
{
    private CollisionMatrix t;

    private void OnEnable()
    {
        t = target as CollisionMatrix;
        SceneView.duringSceneGui -= OnScene;
        SceneView.duringSceneGui += OnScene;
    }

    void OnScene(SceneView sceneview)
    {
        if (t.showSceneBounds)
        {
            DrawMatrixBounds();
        }
    }

    private void DrawMatrixBounds()
    {

        Vector3 origin = t.origin - Vector3.one * 0.5f;
        Vector3[] verts;
        float matrixWidth = t.matrixSize.x;
        float matrixHeight = t.matrixSize.y;

        // ISOMETRIC
        if (t.mode == CollisionMatrix.Mode.ISOMETRIC)
        {
            verts = new Vector3[]
            {
            new Vector3(origin.x, 0, origin.z),
            new Vector3(origin.x, 0, origin.z + matrixHeight),
            new Vector3(origin.x + matrixWidth, 0, origin.z + matrixHeight),
            new Vector3(origin.x + matrixWidth, 0, origin.z),
            };
        }
        // TOP DOWN
        else
        {
            verts = new Vector3[]
            {
            new Vector3(origin.x, origin.y),
            new Vector3(origin.x, origin.y + matrixHeight),
            new Vector3(origin.x + matrixWidth, origin.y + matrixHeight),
            new Vector3(origin.x + matrixWidth, origin.y),
            };
        }

        Handles.DrawSolidRectangleWithOutline(verts, t.sceneBoundsColor, new Color(0, 0, 0, 1));
    }

    public override void OnInspectorGUI()
    {
        // Show default inspector property editor
        DrawDefaultInspector();

        if (GUILayout.Button("Realign Transforms on grid"))
        {
            RealignTransformsOnGrids();
        }

        if (GUILayout.Button("Center Origin"))
        {
            t.CenterOrigin();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(t);
            EditorSceneManager.MarkSceneDirty(t.gameObject.scene);
        }
    }

    // add anotation to make it show in top menu
    [MenuItem("Tools/Realign entities on grid")]
    private static void RealignTransformsOnGrids()
    {

        float gridPace = 0.5f;

        Debug.Log("Realign all transforms on the grid");

        MatrixCollider[] colliders = FindObjectsOfType<MatrixCollider>();
        foreach (MatrixCollider collider in colliders)
        {
            Vector3 initPos = collider.transform.position;
            Vector3 newPos = new Vector3(
                Mathf.Round(initPos.x / gridPace) * gridPace,
                0f,
                Mathf.Round(initPos.z / gridPace) * gridPace
            );
            collider.transform.position = newPos;
        }
    }
}