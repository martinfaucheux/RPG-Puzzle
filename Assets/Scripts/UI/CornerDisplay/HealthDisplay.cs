using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : UIBarDisplay
{
    public static HealthDisplay instance = null;

    [SerializeField] ShakeUI _shaker;
    private Health _healthComponent;
    private int _previousHealth;


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

    protected override void Start()
    {
        _healthComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        GameEvents.instance.onHealthChange += OnPlayerHealthChange;
        base.Start();
    }

    private void OnPlayerHealthChange(int healthID, int initValue, int finalValue)
    {
        if (healthID == _healthComponent.GetInstanceID())
            UpdateUI();
    }

    protected override int GetNewImageCount()
    {
        if (_healthComponent != null)
        {
            return _healthComponent.MaxHealthPoints;
        }
        return 0;
    }


    protected override void UpdateElementStates()
    {
        HeartUI[] heartComponents = imageContainerTransform.GetComponentsInChildren<HeartUI>();
        int imageCount = heartComponents.Length;

        for (int i = 0; i < imageCount; i++)
        {
            if (i < _healthComponent.CurrentHealthPoints)
                heartComponents[i].SetToFull();

            else if (i < _healthComponent.MaxHealthPoints)
                heartComponents[i].SetToEmpty();

            else
                heartComponents[i].Hide();
        }

        if (_healthComponent.CurrentHealthPoints < _previousHealth)
            _shaker.Shake();
        _previousHealth = _healthComponent.CurrentHealthPoints;
    }
}
