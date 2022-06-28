using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorUtils
{
    public static bool HasParameter(Animator animator, string paramName)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
                return true;
        }
        return false;
    }
}
