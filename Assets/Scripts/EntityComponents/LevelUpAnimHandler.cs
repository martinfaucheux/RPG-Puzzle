using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpAnimHandler : MonoBehaviour
{
    public SpriteFlasher spriteFlasher;
    public Animator animator;
    

    void Start()
    {
        GameEvents.instance.onEnterLevelUp += AnimateLevelUp;
    }
    
    void Update(){
        if (Input.GetKeyDown(KeyCode.P))
            AnimateLevelUp();
    }

    private void AnimateLevelUp(){
        spriteFlasher.Flash();
        animator.SetTrigger("levelup");
    }
}
