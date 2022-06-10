using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDescriptionUpdater : MonoBehaviour
{

    // DEPRECATED => use SkillSelectorUI instead
    
    public static SkillDescriptionUpdater instance;
    public Image backgroundImageComponent;
    public Text skillNameTextComponent;
    public Text skillDescriptionTextComponent;


    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a CollisionMatrix.
            Destroy(gameObject);
    }

    void Start(){
        CleanSkillInformation();
    }

    public void SetSkillInformation(Skill skill){
        backgroundImageComponent.enabled = true;
        skillNameTextComponent.text = skill.skillName;
        skillDescriptionTextComponent.text = skill.skillDescription;
    }

    public void CleanSkillInformation(){
        backgroundImageComponent.enabled = false;
        skillNameTextComponent.text = "";
        skillDescriptionTextComponent.text = "";
    }
}
