
using UnityEngine;
using System;

public abstract class Skill: ScriptableObject
{
    public SkillCategory skillCategory;
    public Sprite sprite;

    public string skillName;
    public string skillDescription;

    // to be DEPRECATED
    // this information will now be stored in SkillManager

    public virtual void Enable(GameObject target){
        GameEvents.instance.SkillEnabledTrigger(this);
    }

    public virtual void Disable(GameObject target){

    }

    protected T GetComponentOrRaise<T>(GameObject target){
        T component = target.GetComponent<T>();
        
        if (component == null)
            Debug.LogWarning("Missing component: " + typeof(T).Name);
        
        return component;
    }
}