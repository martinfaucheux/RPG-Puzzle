using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectTileInitializer : MonoBehaviour
{
    [SerializeField] LevelMetaDataCollection _levelCollection;
    [SerializeField] GameObject _levelSelectTilePrefab;
    [SerializeField] LevelObjectiveList _levelObjectiveList;
    [SerializeField] LevelSelectManager _levelSelectManager;
    private GenericGrid<LevelMetaData> _levelGrid;

    void Start()
    {
        _levelGrid = _levelCollection.GetLevelGrid();
    }

    public void InstantiateTiles()
    {
        bool isFirstTile = true;
        foreach (LevelMetaData levelData in _levelCollection.levelList)
        {
            Vector3 realWordPosition = LevelSelectManager.GetRealWorldPosition(levelData.overWorldPostion);
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
                _levelSelectManager.PlacePlayer(realWordPosition);
                _levelSelectManager.firstTile = levelSelectTile;
                isFirstTile = false;
            }
        }
    }




}
