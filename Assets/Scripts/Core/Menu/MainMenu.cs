using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;

    public GameObject mainMenuGO;
    public GameObject optionMenuGO;
    public GameObject backgroundGO;

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

    public virtual void Start(){

        if (mainMenuGO == null)
            Debug.Log("No mainMenuGO provided");
        
        if (optionMenuGO == null)
            Debug.Log("No optionMenuGO provided");
    }

    public void ExitGame(){
        LevelLoader.instance.Quit();
    }

    public void ToggleMainMenu(){
        mainMenuGO.SetActive(true);
        optionMenuGO.SetActive(false);
    }

    public void ToggleOptionMenu(){
        mainMenuGO.SetActive(false);
        optionMenuGO.SetActive(true);
    }
}
