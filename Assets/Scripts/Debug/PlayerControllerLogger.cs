using UnityEngine;

public class PlayerControllerLogger : MonoBehaviour
{
    private void Awake()
    {
        PlayerController.OnGetCommand += TriggerLog;
    }

    private void TriggerLog(Direction direction)
    {
        Debug.Log("Command: " + direction);
    }
}