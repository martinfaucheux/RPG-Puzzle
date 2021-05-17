using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{

    public static SkillManager instance;

    [SerializeField] private int _skillPoint;

    public int skillPoint {
        get {return _skillPoint;}
        private set {_skillPoint = value;}
    }

    private List<Skill> _skills;

    private Dictionary<Skill, bool> _skillDict;

    //Awake is always called before any Start functions
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

        _skills = new List<Skill>();
        _skillDict = new Dictionary<Skill, bool>();
        GameEvents.instance.onEnterLevelUp += AddSkillPoint;
    }

    public void AddSkill (Skill skill){
        _skills.Add(skill);
        _skillDict.Add(skill, false);
    }

    public void AddSkillPoint(){
        skillPoint += 1;
    }

    public bool Enable(Skill skill){
        if (skillPoint > 0){
            skillPoint -= 1;

            _skillDict[skill] = true;

            // TODO: find a better way to reference user
            GameObject playerGo = GameObject.FindGameObjectWithTag("Player");
            if (playerGo)
                skill.Enable(playerGo);
            return true;
        }
        return false;
    }

    public bool IsUnlocked(Skill skill){
        return _skillDict[skill];
    }
}
