using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;


public class PlayInputManager : SingletoneBase<PlayInputManager>
{
    public static event Action<Direction> OnGetCommand = delegate { };
    [SerializeField] PlayerInput playerInput;
    [SerializeField] float directionalInputRotation = -90f;
    private PlayerInputActions playerInputActions;
    private Dictionary<string, InputActionMap> _inputActionmaps;

    protected override void Awake()
    {
        base.Awake();
        playerInputActions = new PlayerInputActions();
        _inputActionmaps = new Dictionary<string, InputActionMap>();
        _inputActionmaps["Player"] = playerInputActions.Player;
        _inputActionmaps["UI"] = playerInputActions.UI;

        playerInputActions.Player.Enable();

        // from Play mode
        playerInputActions.Player.Movement.performed += ProcessMovement;
        playerInputActions.Player.Pause.performed += EnterPause;
        playerInputActions.Player.Restart.performed += Restart;
        playerInputActions.Player.Continue.performed += EnterPostWin;
        playerInputActions.Player.UnlockSkill1.performed += UnlockSkill;
        playerInputActions.Player.UnlockSkill2.performed += UnlockSkill;
        playerInputActions.Player.UnlockSkill3.performed += UnlockSkill;

        // from UI mode
        playerInputActions.UI.Exit.performed += ExitPause;
    }

    public void SwitchCurrentActionMap(string mapName)
    {
        foreach (KeyValuePair<string, InputActionMap> kv in _inputActionmaps)
        {
            kv.Value.Disable();
        }
        _inputActionmaps[mapName].Enable();
    }

    void ProcessMovement(InputAction.CallbackContext context)
    {
        if (StateManager.instance.currentGameState == GameState.PLAY)
        {
            Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
            inputVector = Quaternion.Euler(0, 0, directionalInputRotation) * inputVector;

            if (inputVector != Vector2.zero)
            {
                Direction direction = Direction.GetFromCoord(Vector2Int.RoundToInt(inputVector));
                OnGetCommand(direction);
            }
        }
    }

    void EnterPause(InputAction.CallbackContext context) => GameManager.instance.EnterPause();
    void ExitPause(InputAction.CallbackContext context) => GameManager.instance.ExitPause();
    void Restart(InputAction.CallbackContext context) => GameManager.instance.Restart();
    void EnterPostWin(InputAction.CallbackContext context) => GameManager.instance.EnterPostWin();
    void UnlockSkill(InputAction.CallbackContext context)
    {
        if (StateManager.instance.currentGameState == GameState.PLAY)
        {

            int skillId = 0;
            switch (context.action.name)
            {
                case "UnlockSkill1":
                    skillId = 0;
                    break;
                case "UnlockSkill2":
                    skillId = 1;
                    break;
                case "UnlockSkill3":
                    skillId = 2;
                    break;
                default:
                    Debug.LogError("Unknown skill id");
                    break;
            }
            SkillManager.instance.Unlock(skillId);
        }
    }
}
