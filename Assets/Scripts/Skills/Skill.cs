
using UnityEngine;
using System;

public abstract class Skill: ScriptableObject
{
    public SkillCategory skillCategory;
    public Sprite sprite;

    public string skillName;
    public string skillDescription;
    public bool isEnabled {get; private set;} = false;

    public virtual void Enable(GameObject target){
        isEnabled = true;
        GameEvents.instance.SkillEnabledTrigger();
    }

    public virtual void Disable(GameObject target){
        isEnabled = false;
    }
}