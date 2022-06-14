using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(EnvironmentDrawer))]
public class EnvironmentDrawerCustomInspector : Editor
{
    private static string borderWallsGOName = "BorderWalls";

    private EnvironmentDrawer t;
    private CollisionMatrix matrix;

    private void OnEnable()
    {
        t = target as EnvironmentDrawer;
        matrix = t.GetComponent<CollisionMatrix>();
    }

    public override void OnInspectorGUI()
    {
        // Show default inspector property editor
        DrawDefaultInspector();

        if (GUILayout.Button("Create Grid"))
        {
            InstantiateGrid();
        }

        if (GUILayout.Button("Create Border Ribbons"))
        {
            InstantiateRibbon();
        }

        if (GUILayout.Button("Create Decoration"))
        {
            InstantiateDecoration();
        }

        // if (GUILayout.Button("Build Border Walls"))
        // {
        //     BuildBorderWalls();
        // }

        // if (GUILayout.Button("Destroy Border Walls"))
        // {
        //     DestroyBorderWalls();
        // }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(t);
            EditorSceneManager.MarkSceneDirty(t.gameObject.scene);
        }
    }

    public void BuildBorderWalls()
    {
        GameObject borderWallsGO = GetBorderGO();

        int matrixWidth = matrix.matrixSize.x;
        int matrixHeight = matrix.matrixSize.y;

        Vector3 origin = matrix.origin;

        foreach (float xPos in new float[] { -1f, matrixWidth })
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

    private void InstantiateGrid()
    {
        Vector3 constantVect = 1 * new Vector3(1, -0.82f, 1);
        GameObject gridContainer = GetOrInstiateEmpty("Grid", true);

        for (float x = 0; x < matrix.matrixSize.x; x++)
        {
            for (float y = 0; y < matrix.matrixSize.y; y++)
            {
                if (t.checkerBoard && (x + y) % 2 == 0)
                {
                    continue;
                }

                Vector3 position = matrix.GetRealWorldPosition(new Vector2(x, y));
                GameObject gridUnitGO = Instantiate(t.gridUnitPrefab, position + constantVect, Quaternion.identity);
                gridUnitGO.transform.SetParent(gridContainer.transform);
            }
        }
    }

    // <summary>
    // instantiate a ribbon GameObject at the border of the walkable area
    // with the proper rotation
    // </summary>
    private void InstantiateRibbon()
    {
        Vector3 matrixSize = to3d(matrix.matrixSize);
        Transform container = GetOrInstiateEmpty("BorderRibbons", true).transform;

        foreach (Direction direction in Direction.GetAll<Direction>())
        {
            if (direction == Direction.IDLE) continue;

            Vector2Int dirVect2d = direction.ToPos();

            float distance, borderSize, angle;
            if (dirVect2d.x == 0)
            {
                distance = matrix.matrixSize.y;
                borderSize = matrix.matrixSize.x;
                angle = 90;
            }
            else
            {
                distance = matrix.matrixSize.x;
                borderSize = matrix.matrixSize.y;
                angle = 0;
            }


            Vector3 dirVect = to3d(dirVect2d);
            Vector3 pos = (0.3f + 0.5f * distance) * dirVect - new Vector3(0.5f, 0f, 0.5f);

            Quaternion rotation = Quaternion.Euler(90f, 0, angle);

            GameObject ribbonGO = Instantiate(t.BorderRibbonPrefab, pos, rotation, container);
            SpriteRenderer spriteRenderer = ribbonGO.GetComponent<SpriteRenderer>();
            spriteRenderer.size = new Vector2(0.5f, 0.5f + borderSize);
        }
    }


    public void InstantiateDecoration()
    {
        Transform container = GetOrInstiateEmpty("Decoration", true).transform;

        float xBorderMin = -matrix.matrixSize.x / 2 - t.minDecorationDistance;
        float xBorderMax = matrix.matrixSize.x / 2 + t.minDecorationDistance;
        float yBorderMin = -matrix.matrixSize.y / 2 - t.minDecorationDistance;
        float yBorderMax = matrix.matrixSize.y / 2 + t.minDecorationDistance;

        float xMin = xBorderMin - t.maxDecorationDistance;
        float xMax = xBorderMax + t.maxDecorationDistance;
        float yMin = yBorderMin - t.maxDecorationDistance;
        float yMax = yBorderMax + t.maxDecorationDistance;


        // Vector3[] verts = new Vector3[]{
        //     new Vector3(xMin, 0, yMin),
        //     new Vector3(xMin, 0, yMax),
        //     new Vector3(xMax, 0, yMax),
        //     new Vector3(xMax, 0, yMin)
        // };

        // Handles.DrawSolidRectangleWithOutline(
        //     verts,
        //     matrix.sceneBoundsColor,
        //     new Color(0, 0, 0, 1)
        // );


        for (float x = xMin; x <= xMax; x++)
        {
            for (float y = yMin; y <= yMax; y++)
            {
                // if within walkable space
                if (
                    xBorderMin <= x && x <= xBorderMax
                    && yBorderMin <= y && y <= yBorderMax
                )
                    continue;

                // leave empty spaces based on density
                if (Random.Range(0f, 1) > t.decorationDensity)
                    continue;

                int prefabIndex = Random.Range(0, t.decorationPrefabs.Length);
                GameObject decoGO = Instantiate(
                    t.decorationPrefabs[prefabIndex],
                    new Vector3(x, 0f, y),
                    Quaternion.identity,
                    container
                );
                CleanGameObject(decoGO);
            }
        }
    }

    private static void CleanGameObject(GameObject gameObject)
    {
        MatrixCollider collider = gameObject.GetComponent<MatrixCollider>();
        if (collider != null)
        {
            DestroyImmediate(collider);
        }
        EntityInspector inspector = gameObject.GetComponent<EntityInspector>();
        if (inspector != null)
        {
            DestroyImmediate(inspector);
        }
    }


    private static Vector3 to3d(Vector2Int pos)
    {
        return new Vector3(pos.x, 0f, pos.y);
    }

    private static GameObject GetOrInstiateEmpty(string gameObjectName, bool destroy = false) => GetOrInstiateEmpty(gameObjectName, Vector3.zero, destroy);

    private static GameObject GetOrInstiateEmpty(string gameObjectName, Vector3 position, bool destroy = false)
    {
        GameObject existingGameObject = GameObject.Find(gameObjectName);
        if (existingGameObject != null)
        {
            if (!destroy)
            {
                return existingGameObject;
            }
            else
            {
                DestroyImmediate(existingGameObject);
            }
        }

        GameObject newGameObject = new GameObject(gameObjectName);
        newGameObject.transform.position = position;
        return newGameObject;
    }
}