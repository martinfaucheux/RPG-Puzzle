using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndOfLevelText  : BlinkingText
{
    void Start(){
        GameEvents.instance.onEndOfLevel += DelayShow;
    }

    void OnDestroy(){
        GameEvents.instance.onEndOfLevel -= DelayShow;
    }
}
