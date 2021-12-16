using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : ScriptableObject
{
    public string questName;
    public abstract bool CheckCompletion();

    public virtual void Initialize() { }
}
