using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpButtonSlider : SlidingUI
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GameEvents.instance.onEnterLevelUp += SlideIn;
        GameEvents.instance.onExitLevelUp += SlideOut;
    }
}
