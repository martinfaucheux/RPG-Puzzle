using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;

public class PlayInputManager : SingletoneBase<PlayInputManager>
{
    public static event Action<Direction> OnGetCommand = delegate { };
    [SerializeField] PlayerInput playerInput;
    [SerializeField] float directionalInputRotation = -90f;
    public PlayerInputActions playerInputActions { get; private set; }
    private Dictionary<string, InputActionMap> _inputActionmaps;
    public InputAction[] skillInputActions { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        playerInputActions = new PlayerInputActions();
        PlayerInputActions.PlayerActions playerActions = playerInputActions.Player;

        _inputActionmaps = new Dictionary<string, InputActionMap>();
        _inputActionmaps["Player"] = playerInputActions.Player;
        _inputActionmaps["UI"] = playerInputActions.UI;

        skillInputActions = new InputAction[]{
            playerActions.UnlockSkill1,
            playerActions.UnlockSkill2,
            playerActions.UnlockSkill3,
        };

        playerInputActions.Player.Enable();

        // from Play mode
        playerActions.Movement.performed += ProcessMovement;
        playerActions.Pause.performed += EnterPause;
        playerActions.Restart.performed += Restart;
        playerActions.Continue.performed += EnterPostWin;
        playerActions.UnlockSkill1.performed += UnlockSkill;
        playerActions.UnlockSkill2.performed += UnlockSkill;
        playerActions.UnlockSkill3.performed += UnlockSkill;

        // from UI mode
        playerInputActions.UI.Exit.performed += ExitPause;
    }

    public void SwitchCurrentActionMap(string mapName)
    {
        foreach (KeyValuePair<string, InputActionMap> kv in _inputActionmaps)
            kv.Value.Disable();

        _inputActionmaps[mapName].Enable();
    }

    void ProcessMovement(InputAction.CallbackContext context)
    {
        if (GameManager.instance.playerCanMove)
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
    void EnterPostWin(InputAction.CallbackContext context) => GameManager.instance.ContinueToLevelSelect();
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

    public string GetBindingPath(InputAction inputAction)
    {
        string path = inputAction.bindings[0].path;
        return path.Split('/').Last();
    }
    public string GetSkillBindingPath(int skillId) => GetBindingPath(skillInputActions[skillId]);
}
