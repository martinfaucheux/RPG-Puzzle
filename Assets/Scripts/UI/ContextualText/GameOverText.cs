using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverText : BlinkingText
{
    void Start(){
        GameEvents.instance.onGameOver += DelayShow;
    }

    void OnDestroy(){
        GameEvents.instance.onGameOver -= DelayShow;
    }
}
