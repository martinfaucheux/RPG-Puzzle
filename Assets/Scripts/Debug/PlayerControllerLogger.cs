using UnityEngine;

public class PlayerControllerLogger : MonoBehaviour
{
    private void Awake()
    {
        PlayerController.OnGetCommand += TriggerLog;
    }

    private void TriggerLog(Direction2 direction)
    {
        Debug.Log("Command: " + direction);
    }
}