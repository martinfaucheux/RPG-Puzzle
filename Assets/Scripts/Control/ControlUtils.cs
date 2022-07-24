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
#if ENABLE_LEGACY_INPUT_MANAGER 
        foreach (KeyCode keyCode in keyCodeList)
        {
            if (Input.GetKeyDown(keyCode))
                return false;
        }
        return Input.anyKeyDown;
#endif
#if ENABLE_INPUT_SYSTEM
        return false;
#endif

    }

    public static bool GetArrowKeyDown()
    {
#if ENABLE_LEGACY_INPUT_MANAGER 
        foreach (KeyCode keyCode in keyCodeList)
        {
            if (Input.GetKeyDown(keyCode))
                return true;
        }
#endif
        return false;
    }
}
