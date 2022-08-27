using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectTileInitializer : MonoBehaviour
{
    [SerializeField] LevelMetaDataCollection _levelCollection;
    [SerializeField] GameObject _levelSelectTilePrefab;
    [SerializeField] LevelObjectiveList _levelObjectiveList;
    [SerializeField] MatrixCollider _playerMatrixCollider;
    [SerializeField] LevelSelectManager _levelSelectManager;
    private GenericGrid<LevelMetaData> _levelGrid;

    void Start()
    {
        _levelGrid = _levelCollection.GetLevelGrid();
        // InstantiateTiles();
    }

    public void InstantiateTiles()
    {
        bool isFirstTile = true;
        foreach (LevelMetaData levelData in _levelCollection.levelList)
        {
            Vector3 realWordPosition = GetRealWorldPosition(levelData.overWorldPostion);
            GameObject newObj = Instantiate(
                _levelSelectTilePrefab,
                realWordPosition,
                Quaternion.identity,
                transform
            );

            LevelSelectTile levelSelectTile = newObj.GetComponent<LevelSelectTile>();
            levelSelectTile.SetLevelMetaData(levelData);

            if (isFirstTile)
            {
                PlacePlayer(realWordPosition);
                _levelSelectManager.firstTile = levelSelectTile;
                isFirstTile = false;
            }
        }
    }

    private static Vector3 GetRealWorldPosition(Vector2Int overWorldPos)
    {
        Vector2Int argVect = new Vector2Int(
            overWorldPos.x,
            CollisionMatrix.instance.matrixSize.y - 1 - overWorldPos.y
        );
        return CollisionMatrix.instance.GetRealWorldPosition(argVect);
    }

    private void PlacePlayer(Vector3 realWorldPosition)
    {
        _playerMatrixCollider.transform.position = realWorldPosition;
        _playerMatrixCollider.SyncPosition();
    }
}
