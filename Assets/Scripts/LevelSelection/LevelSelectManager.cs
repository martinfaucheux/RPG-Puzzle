using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelSelectManager : SingletoneBase<LevelSelectManager>
{
    public int selectedLevelId { get; private set; }
    [SerializeField] LevelObjectiveList _levelObjectiveList;
    PlayerInputActions _playerInputActions;
    public LevelSelectTile firstTile;
    [SerializeField] MatrixCollider _playerMatrixCollider;
    [SerializeField] LevelMetaDataCollection _levelCollection;


    protected override void Awake()
    {
        base.Awake();
        StateManager.instance.SetState(GameState.LEVEL_SELECT);

        // register confirm command to load next level
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Confirm.performed += LoadSelectedLevel;
    }

    void Start()
    {
        int levelId = SaveManager.instance.lastPlayedLevel;
        // if levelId is not a valid level, resort to the first levelId
        if (!LevelLoader.IsLevelId(levelId))
            levelId = firstTile.levelData.sceneBuildIndex;

        LevelMetaData levelData = _levelCollection.GetLevelBySceneBuildIndex(levelId);
        Vector3 realWordPosition = GetRealWorldPosition(levelData.overWorldPostion);

        PlacePlayer(realWordPosition);
        SelectLevel(levelId);
    }

    void OnDestroy()
    {
        _playerInputActions.Player.Confirm.performed -= LoadSelectedLevel;
    }

    public void SelectLevel(int levelId)
    {
        _levelObjectiveList.SetLevel(levelId);
        selectedLevelId = levelId;
    }

    public void LoadSelectedLevel() => LevelLoader.instance.LoadLevel(selectedLevelId);

    public void LoadSelectedLevel(InputAction.CallbackContext context)
    {
        if (StateManager.instance.currentGameState == GameState.LEVEL_SELECT)
            LoadSelectedLevel();
    }

    public static Vector3 GetRealWorldPosition(Vector2Int overWorldPos)
    {
        Vector2Int argVect = new Vector2Int(
            overWorldPos.x,
            CollisionMatrix.instance.matrixSize.y - 1 - overWorldPos.y
        );
        return CollisionMatrix.instance.GetRealWorldPosition(argVect);
    }

    public void PlacePlayer(Vector3 realWorldPosition)
    {
        _playerMatrixCollider.transform.position = realWorldPosition;
        _playerMatrixCollider.SyncPosition();
    }
}
