using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMenu : ToggleMenu
{

    public static SkillMenu instance;

    #region Singleton

    private bool canInteract
    {
        get { return !GameManager.instance.isInstruction; }
    }

    // Check singleton
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

    #endregion

    protected override void Hide()
    {
        if (canInteract)
        {
            base.Hide();
            GameEvents.instance.CloseSkillMenuTrigger();
        }
    }

    protected override void Show()
    {
        if (canInteract)
        {
            base.Show();
            GameEvents.instance.OpenSkillMenuTrigger();
        }
    }
}
