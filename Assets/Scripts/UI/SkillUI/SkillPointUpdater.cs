using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillPointUpdater : MonoBehaviour
{
    public bool showOnlyIfMoreThanZero = false;

    public Image badgeImage;
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
        int skillPoint = SkillManager.instance.skillPoint;
        
        if(showOnlyIfMoreThanZero && badgeImage != null){
            bool condition = (skillPoint>0);
            badgeImage.enabled = condition;
            skillPointCounterText.enabled = condition;
        }

        skillPointCounterText.text = skillPoint.ToString();
    }

    void OnDestroy(){
        GameEvents.instance.onEnterLevelUp -= UpdateSkillPointCounter;
        GameEvents.instance.onSkillEnabled -= UpdateSkillPointCounter;
    }
}
