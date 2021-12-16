using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelMetaData", menuName = "Custom Objects / Levels / LevelMetaData")]
public class LevelMetaData : ScriptableObject
{
    public string sceneName;
    public int sceneBuildIndex;
    public int gemCount = 1;
    public List<Quest> quests;
}
