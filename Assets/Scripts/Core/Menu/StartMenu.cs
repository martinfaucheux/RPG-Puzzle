using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MainMenu
{

    public override void Start(){

        base.Start();

        // retrieve data from player.data
        LevelLoader.instance.RetrieveGameState();
    }

    public void PlayGame(){
        LevelLoader.instance.LoadLastLevelPlayed();
    }

}
