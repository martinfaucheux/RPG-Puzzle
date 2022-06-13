using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{

    public static SkillManager instance;

    [SerializeField] private int _skillPoint;

    public int skillPoints
    {
        get { return _skillPoint; }
        private set { _skillPoint = value; }
    }

    private Dictionary<Skill, bool> _unlockedSkills;

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

        _unlockedSkills = new Dictionary<Skill, bool>();
        GameEvents.instance.onLevelUp += AddSkillPoint;
    }

    public void RegisterSkill(Skill skill)
    {
        _unlockedSkills.Add(skill, false);
    }

    public void AddSkillPoint()
    {
        skillPoints += 1;
    }

    public bool Unlock(Skill skill)
    {
        if (skillPoints > 0)
        {
            skillPoints -= 1;

            _unlockedSkills[skill] = true;

            // TODO: find a better way to reference user
            GameObject playerGo = GameObject.FindGameObjectWithTag("Player");
            if (playerGo)
                skill.Enable(playerGo);
            return true;
        }
        return false;
    }

    public bool IsUnlocked(Skill skill)
    {
        return _unlockedSkills[skill];
    }
}
