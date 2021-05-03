using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillPointUpdater : MonoBehaviour
{
    public bool showOnlyIfMoreThanZero = false;

    public Image badgeImage;
    public Text skillPointCounterText;

    [Tooltip("Text to be left-concatenated to the score")]
    public string displayPrefix;
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
        skillPointCounterText.text = displayPrefix + skillPoint.ToString();
        
        if(showOnlyIfMoreThanZero){
            bool condition = (skillPoint > 0);
            skillPointCounterText.enabled = condition;
            
            if(badgeImage != null){
                badgeImage.enabled = condition;
            }
        }
    }

    void OnDestroy(){
        GameEvents.instance.onEnterLevelUp -= UpdateSkillPointCounter;
        GameEvents.instance.onSkillEnabled -= UpdateSkillPointCounter;
    }
}
