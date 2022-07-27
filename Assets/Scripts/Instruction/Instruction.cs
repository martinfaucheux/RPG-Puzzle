using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Instruction", menuName = "Custom Objects / Instruction")]
public class Instruction : ScriptableObject
{
    public string instructionName;
    [TextArea]
    public string description;
}
