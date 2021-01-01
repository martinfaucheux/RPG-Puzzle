using UnityEngine;

public class PlayerControlerLogger : MonoBehaviour
{
    private void Awake()
    {
        PlayerControler.OnGetCommand += TriggerLog;
    }

    private void TriggerLog(Direction direction)
    {
        Debug.Log("Command: " + direction);
    }
}