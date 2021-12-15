using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelMetaData", menuName = "Custom Objects / Levels / LevelMetaData")]
public class LevelMetaData : ScriptableObject
{
    // TODO: NOT USED YET
    public string slug;
    public string sceneName;
    public int gemCount = 1;
    // TODO: add quests here
}
