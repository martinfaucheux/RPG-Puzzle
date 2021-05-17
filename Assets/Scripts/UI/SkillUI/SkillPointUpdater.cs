using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillPointUpdater : MonoBehaviour
{
    public bool showOnlyIfMoreThanZero = false;

    public Image badgeImage;
    public Text skillPointCounterText;
    public TextMeshProUGUI skillPointCounterTMProText;

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
        SetText(displayPrefix + skillPoint.ToString());
        
        if(showOnlyIfMoreThanZero){
            bool condition = (skillPoint > 0);
            SetShow(condition);
            
            if(badgeImage != null){
                badgeImage.enabled = condition;
            }
        }
    }

    // to be compliant with event signature
    private void UpdateSkillPointCounter(Skill _) => UpdateSkillPointCounter();

    private void SetShow(bool show){
        if (skillPointCounterText != null)
            skillPointCounterText.enabled = show;
        if (skillPointCounterTMProText != null)
            skillPointCounterTMProText.enabled = show;
    }

    private void SetText(string text){
        
        if (skillPointCounterText != null)
            skillPointCounterText.text = text;
        if (skillPointCounterTMProText != null)
            skillPointCounterTMProText.text = text;
    }

    void OnDestroy(){
        GameEvents.instance.onEnterLevelUp -= UpdateSkillPointCounter;
        GameEvents.instance.onSkillEnabled -= UpdateSkillPointCounter;
    }
}
