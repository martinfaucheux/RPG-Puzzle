using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    // maximum level id that was reached
    public int maxLevelId;

    // current active level
    public int  currentLevelId;

    public PlayerData(int maxLevelId, int currentLevelId){
        this.maxLevelId = maxLevelId;
        this.currentLevelId = currentLevelId;
    }

    public PlayerData(LevelLoader levelLoader):
        this(
            levelLoader.maxLevelId,
            levelLoader.currentLevelId
        ){}

}
