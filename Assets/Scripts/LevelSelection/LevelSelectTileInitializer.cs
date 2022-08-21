using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectTileInitializer : MonoBehaviour
{
    [SerializeField] LevelMetaDataCollection _levelCollection;
    [SerializeField] GameObject _levelSelectTilePrefab;
    [SerializeField] LevelObjectiveList _levelObjectiveList;
    [SerializeField] Color _lockedColor;
    [SerializeField] Color _availableColor;
    [SerializeField] Color _gemsCollectedColor;
    [SerializeField] Color _questsCompletedColor;
    [SerializeField] MatrixCollider _playerMatrixCollider;
    private GenericGrid<LevelMetaData> _levelGrid;

    void Start()
    {
        _levelGrid = _levelCollection.GetLevelGrid();
        InstantiateTiles();
    }

    private void InstantiateTiles()
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
            levelSelectTile.Initialize(levelData, _levelObjectiveList, GetColor(levelData));

            if (isFirstTile)
            {
                PlacePlayer(realWordPosition);
                _levelObjectiveList.SetLevel(levelData.sceneBuildIndex);
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

    private Color GetColor(LevelMetaData levelData)
    {
        int levelId = levelData.sceneBuildIndex;
        PlayerData playerData = LevelLoader.instance.playerSavedData;

        bool isUnlocked = playerData.IsUnlocked(levelId);
        int collectedGems = playerData.GetCollectedGemCount(levelId);
        int completedQuests = playerData.GetCompletedQuestsCount(levelId);

        if (!isUnlocked)
            return _lockedColor;

        if (collectedGems == levelData.gemCount)
        {
            if (completedQuests == levelData.quests.Count)
                return _questsCompletedColor;
            else
                return _gemsCollectedColor;
        }
        else
            return _availableColor;
    }
}
