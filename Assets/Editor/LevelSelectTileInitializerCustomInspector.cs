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
        }
    }

    private void InstantiateTiles()
    {
        foreach (Transform _transform in t.transform)
            DestroyImmediate(_transform.gameObject);

        bool isFirstTile = true;

        int levelCount = t.levelCollection.levelList.Count;
        for (int levelId = 0; levelId < levelCount; levelId++)
        {
            LevelMetaData levelData = t.levelCollection.levelList[levelId];

            Vector3 realWordPosition = LevelSelectManager.GetRealWorldPosition(levelData.overWorldPostion);

            GameObject newObj = PrefabUtility.InstantiatePrefab(t.levelSelectTilePrefab) as GameObject;
            newObj.transform.position = realWordPosition;
            newObj.transform.SetParent(t.transform);

            LevelSelectTile levelSelectTile = newObj.GetComponent<LevelSelectTile>();
            levelSelectTile.SetLevelMetaData(levelData);

            if (isFirstTile)
            {
                t.levelSelectManager.PlacePlayer(realWordPosition);
                t.levelSelectManager.firstTile = levelSelectTile;
                isFirstTile = false;
            }

            EditorUtility.SetDirty(newObj);
            PrefabUtility.RecordPrefabInstancePropertyModifications(newObj);
        }
    }
}