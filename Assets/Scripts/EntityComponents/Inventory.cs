using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public int keyCount { get; private set; } = 0;

    public void AddKey() => keyCount++;
    public bool UseKey()
    {
        if (keyCount > 0)
        {
            keyCount--;
            return true;
        }
        Debug.LogError("Tried to use unexisting key");
        return false;
    }
}
