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

        if (GUILayout.Button("Create Grid"))
        {
            InstantiateGrid();
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
            GameObject envGO = GetOrInstiateEmpty("Environment");

            borderWallsGO = new GameObject();
            borderWallsGO.transform.SetParent(envGO.transform);
            borderWallsGO.name = borderWallsGOName;
        }

        return borderWallsGO;
    }

    // add anotation to make it show in top menu
    [MenuItem ("Tools/Realign entities on grid" )]
    private static void RealignTransformsOnGrids(){

        float gridPace = 0.5f;

        Debug.Log("Realign all transforms on the grid");

        MatrixCollider[] colliders = FindObjectsOfType<MatrixCollider>();
        foreach(MatrixCollider collider in colliders){
            Vector3 initPos = collider.transform.position;
            Vector3 newPos =  new Vector3(
                Mathf.Round(initPos.x / gridPace) * gridPace,
                0f,
                Mathf.Round(initPos.z / gridPace) * gridPace
            );
            collider.transform.position = newPos;
        }
    }

    private void InstantiateGrid(){

        Vector3 constantVect = 1 * new Vector3(1, -0.82f, 1);
        GameObject gridContainer = GetOrInstiateEmpty("Grid");

        for(float x = 0; x < t.matrixSize.x; x++){
            for(float y = 0; y < t.matrixSize.y; y++){
                Vector3 position = t.GetRealWorldPosition(new Vector2(x, y));
                GameObject gridUnitGO = Instantiate(t.GridUnitPrefab, position + constantVect, Quaternion.identity);
                gridUnitGO.transform.SetParent(gridContainer.transform);
            }
        }
    }

    private static GameObject GetOrInstiateEmpty(string gameObjectName) => GetOrInstiateEmpty(gameObjectName, Vector3.zero);

    private static GameObject GetOrInstiateEmpty(string gameObjectName, Vector3 position){
        GameObject newGameObject = GameObject.Find(gameObjectName);

        

        if (newGameObject == null)
        {
            Debug.Log("could not find " + gameObjectName + ", instantiate it");
            newGameObject = new GameObject(gameObjectName);
            newGameObject.transform.position = position;
        }
        return newGameObject;
    }
}