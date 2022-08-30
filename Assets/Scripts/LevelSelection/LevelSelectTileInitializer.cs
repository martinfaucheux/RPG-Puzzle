using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectTileInitializer : MonoBehaviour
{
    public LevelMetaDataCollection levelCollection;
    public GameObject levelSelectTilePrefab;
    public LevelObjectiveList levelObjectiveList;
    public LevelSelectManager levelSelectManager;
    public GenericGrid<LevelMetaData> levelGrid;

    void Start()
    {
        levelGrid = levelCollection.GetLevelGrid();
    }

    public void InstantiateTiles()
    {
        bool isFirstTile = true;
        foreach (LevelMetaData levelData in levelCollection.levelList)
        {
            Vector3 realWordPosition = LevelSelectManager.GetRealWorldPosition(levelData.overWorldPostion);
            GameObject newObj = Instantiate(
                levelSelectTilePrefab,
                realWordPosition,
                Quaternion.identity,
                transform
            );

            LevelSelectTile levelSelectTile = newObj.GetComponent<LevelSelectTile>();
            levelSelectTile.SetLevelMetaData(levelData);

            if (isFirstTile)
            {
                levelSelectManager.PlacePlayer(realWordPosition);
                levelSelectManager.firstTile = levelSelectTile;
                isFirstTile = false;
            }
        }
    }




}
