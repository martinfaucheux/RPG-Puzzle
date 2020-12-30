using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDisplay : MonoBehaviour {

    public static LevelDisplay instance;

    public DigitUI[] digitUIVect;
    private Experience _expComponent;

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
    }

    // Use this for initialization
    void Start () {
		if (digitUIVect.Length < 3)
        {
            Debug.LogError(this.ToString() + ": some DigitUI missing");
        }
        // TODO change event to listen
        GameEvents.instance.onHealthChange += OnPlayerExperienceChange;
        _expComponent = Player.instance.GetComponent<Experience>();
        UpdateUI();
    }

    private void OnPlayerExperienceChange(int _)
    {
        UpdateUI();
    }

    public void UpdateUI()
    {

        if (IsReady())
        {
            int currentLevel = _expComponent.currentLevel;

            if (currentLevel < 10)
            {
                digitUIVect[0].SetValue(currentLevel);
                digitUIVect[1].Hide();
                digitUIVect[2].Hide();
            }
            else if (currentLevel < 100)
            {
                digitUIVect[0].SetValue(currentLevel / 10);
                digitUIVect[1].SetValue(currentLevel % 10);
                digitUIVect[2].Hide();
            }
            else
            {
                digitUIVect[0].SetValue(currentLevel / 100);
                int decimals = currentLevel % 100;
                digitUIVect[1].SetValue(decimals / 10);
                digitUIVect[2].SetValue(decimals % 10);
            }
        }
    }

    private bool IsReady()
    {
        if (_expComponent == null)
        {
            _expComponent = Player.instance.GetComponent<Experience>();
        }
        return (_expComponent != null);
    }
}
