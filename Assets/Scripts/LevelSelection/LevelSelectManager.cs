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
        LevelLoader.instance.RetrieveGameState();
        SelectLevel(firstTile.levelData.sceneBuildIndex);
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
}
