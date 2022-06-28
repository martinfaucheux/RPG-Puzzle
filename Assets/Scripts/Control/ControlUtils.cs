using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControlUtils
{
    public static List<KeyCode> keyCodeList = new List<KeyCode>(){
            KeyCode.UpArrow,
            KeyCode.DownArrow,
            KeyCode.RightArrow,
            KeyCode.LeftArrow,
        };

    public static bool GetAnyButArrowKeyDown()
    {
        foreach (KeyCode keyCode in keyCodeList)
        {
            if (Input.GetKeyDown(keyCode))
                return false;
        }

        return Input.anyKeyDown;
    }

    public static bool GetArrowKeyDown()
    {
        foreach (KeyCode keyCode in keyCodeList)
        {
            if (Input.GetKeyDown(keyCode))
                return true;
        }
        return false;
    }

}
