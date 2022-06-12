using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControlUtils
{
    public static bool GetAnyButArrowKeyDown()
    {

        List<KeyCode> keyCodeList = new List<KeyCode>(){
            KeyCode.UpArrow,
            KeyCode.DownArrow,
            KeyCode.RightArrow,
            KeyCode.LeftArrow,
        };

        foreach (KeyCode keyCode in keyCodeList)
        {
            if (Input.GetKeyDown(keyCode))
            {
                return false;
            }
        }

        return Input.anyKeyDown;
    }
}