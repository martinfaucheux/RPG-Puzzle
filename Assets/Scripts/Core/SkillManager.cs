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
    }

    public void AddSkill (Skill skill){
        _skills.Add(skill);
    }

    public void AddSkillPoint(){
        skillPoint += 1;
    }

    public void Enable(Skill skill){
        if (skillPoint > 0){
            skillPoint -= 1;
            GameObject playerGo = GameObject.FindGameObjectWithTag("Player");
            skill.Enable(playerGo);
        }
    }
}
