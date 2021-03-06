using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillPointUpdater : MonoBehaviour
{
    public Text skillPointCounterText;
    void Start()
    {
        GameEvents.instance.onEnterLevelUp += UpdateSkillPointCounter;
        GameEvents.instance.onSkillEnabled += UpdateSkillPointCounter;
    }

    void OnEnable(){
        UpdateSkillPointCounter();
    }

    public void UpdateSkillPointCounter(){
        skillPointCounterText.text = SkillManager.instance.skillPoint.ToString();
    }

    void OnDestroy(){
        GameEvents.instance.onEnterLevelUp -= UpdateSkillPointCounter;
        GameEvents.instance.onSkillEnabled -= UpdateSkillPointCounter;
    }
}
