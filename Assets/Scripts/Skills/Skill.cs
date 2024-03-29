﻿
using UnityEngine;
using System;

public abstract class Skill : ScriptableObject
{
    public SkillCategory skillCategory;
    public Sprite sprite;

    public string skillName;
    [TextArea]
    public string skillDescription;

    public virtual void Enable(GameObject target)
    {
        GameEvents.instance.SkillEnabledTrigger(this);
    }

    public virtual void Disable(GameObject target) { }

    protected T GetComponentOrRaise<T>(GameObject target)
    {
        T component = target.GetComponent<T>();

        if (component == null)
            Debug.LogWarning("Missing component: " + typeof(T).Name);

        return component;
    }
}