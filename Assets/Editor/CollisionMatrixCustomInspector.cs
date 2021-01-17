using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(CollisionMatrix))]
public class CollisionMatrixCustomInspector : Editor
{
    private static string borderWallsGOName = "BorderWalls";

    private CollisionMatrix t;

    private void OnEnable() {
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

    private void DrawMatrixBounds(){
        
        Vector3 origin = t.origin - Vector3.one * 0.5f;
        Vector3[] verts;
        float matrixWidth = t.matrixSize.x;
        float matrixHeight = t.matrixSize.y;

        // ISOMETRIC
        if (t.mode == CollisionMatrix.Mode.ISOMETRIC){
            verts = new Vector3[]
            {
            new Vector3(origin.x, 0, origin.z),
            new Vector3(origin.x, 0, origin.z + matrixHeight),
            new Vector3(origin.x + matrixWidth, 0, origin.z + matrixHeight),
            new Vector3(origin.x + matrixWidth, 0, origin.z),
            };
        }
        // TOP DOWN
        else {
            verts= new Vector3[]
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

        if (GUILayout.Button("Build Border Walls"))
        {
            BuildBorderWalls();
        }

        if (GUILayout.Button("Destroy Border Walls"))
        {
            DestroyBorderWalls();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(t);
            EditorSceneManager.MarkSceneDirty(t.gameObject.scene);
        }
    }

// [MenuItem("Custom/Build Border Walls")]
public void BuildBorderWalls()
    {
        GameObject borderWallsGO = GetBorderGO();

        int matrixWidth = t.matrixSize.x;
        int matrixHeight = t.matrixSize.y;

        Vector3 origin = t.origin;

        foreach (float xPos in new float[] {-1f, matrixWidth})
        {
            for (float yPos = -1f; yPos <= matrixHeight; yPos += 1f)
            {
                Vector3 wallPosition = origin + new Vector3(xPos, yPos, 0f);
                GameObject newWallGO = Instantiate(t.borderWallPrefab, wallPosition, Quaternion.identity);
                newWallGO.transform.SetParent(borderWallsGO.transform);
            }
        }

        foreach (float yPos in new float[] { -1f, matrixHeight })
        {
            for (float xPos = 0f; xPos < matrixWidth; xPos += 1f)
            {
                Vector3 wallPosition = origin + new Vector3(xPos, yPos, 0f);
                GameObject newWallGO = Instantiate(t.borderWallPrefab, wallPosition, Quaternion.identity);
                newWallGO.transform.SetParent(borderWallsGO.transform);
            }
        }
    }

    // [MenuItem("Custom/Build Border Walls")]
    public void DestroyBorderWalls()
    {
        GameObject borderWallsGO = GetBorderGO();
        DestroyImmediate(borderWallsGO);
    }

    private static GameObject GetBorderGO()
    {
        GameObject borderWallsGO = GameObject.Find(borderWallsGOName);

        if (borderWallsGO == null)
        {
            GameObject envGO = GameObject.Find("Environment");

            if (envGO == null)
            {
                Debug.Log("could not find Env GO, instantiate it");
                envGO = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
                envGO.name = "Environment";
            }

            borderWallsGO = new GameObject();
            borderWallsGO.transform.SetParent(envGO.transform);
            borderWallsGO.name = borderWallsGOName;
        }

        return borderWallsGO;
    }

    public static void RealignTransformsOnGrids(){

        float gridPace = 0.5f;

        float _multiplier = 1 / gridPace;


        Debug.Log("Realign all transforms on the grid");

        MatrixCollider[] colliders = FindObjectsOfType<MatrixCollider>();
        foreach(MatrixCollider collider in colliders){
            Vector3 initPos = collider.transform.position;
            collider.transform.position = new Vector3(
                Mathf.Round(initPos.x * _multiplier) / _multiplier,
                Mathf.Round(initPos.y * _multiplier) / _multiplier,
                initPos.z
            );
        }
    }
}