using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpDisplay : UIBarDisplay
{
    public static ExpDisplay instance = null;

    public GameObject expBallPrefab;

    private Experience _experienceComponent;
    private RectTransform _rectTransform;

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
        _experienceComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
        GameEvents.instance.onPlayerExperienceChange += OnPlayerExperienceChange;
        base.Start();
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.T)){

            //Vector3 newPos = GetWorldPosition();

            // Vector3 newPos = Camera.main.ScreenToWorldPoint(_rectTransform.transform.position);

            Vector3 newPos = GetBoxWorldPosition(0);

            Instantiate(expBallPrefab, newPos, Quaternion.identity);
        }
    }

    private void OnPlayerExperienceChange()
    {
        UpdateUI();
    }

    protected override int GetNewImageCount()
    {
        if (_experienceComponent != null)
        {
            return _experienceComponent.targetExpPoints;
        }
        return 0;
    }

    protected override void UpdateElementStates()
    {
        ExpUI[] expUIComponents = imageContainerTransform.GetComponentsInChildren<ExpUI>();
        int imageCount = expUIComponents.Length;

        for (int i = 0; i < imageCount; i++)
        {
            if (i < _experienceComponent.currentExpPoints)
                expUIComponents[i].SetValue(true);

            else if (i < _experienceComponent.targetExpPoints)
                expUIComponents[i].SetValue(false);

            else
                expUIComponents[i].Hide();
        }
    }

    public void SpawnExpBall(Transform spawnTransform, int previousExpPoints){
        Vector3 targetPos = GetBoxWorldPosition(previousExpPoints);
        Instantiate(expBallPrefab, spawnTransform);
    }

    public Vector3 GetBoxWorldPosition(int index){
        ExpUI[] expUIComponents = imageContainerTransform.GetComponentsInChildren<ExpUI>();
        ExpUI expUIComponent = expUIComponents[index];

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(expUIComponent.transform.position);
        worldPosition.z = 0f;
        return worldPosition;
    }


}
