using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHealthContainer : UIEntityComponent
{

    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;
    private Health _healthComponent;
    [SerializeField] Transform heartContainer;

    protected override void Start()
    {
        base.Start();
    }

    public override void AttachObject(GameObject attachedObject)
    {
        base.AttachObject(attachedObject);
        _healthComponent = attachedObject.GetComponent<Health>();
        if (_healthComponent == null)
        {
            gameObject.SetActive(false);
            return;
        }
        else
        {
            gameObject.SetActive(true);

        }
        UpdateUI();
    }

    public override void UpdateUI()
    {
        // instantiate missing images
        int missingImageCount = _healthComponent.MaxHealthPoints - heartContainer.childCount;
        for (int i = 0; i < missingImageCount; i++)
        {
            GameObject newGameObject = Instantiate(heartContainer.GetChild(0).gameObject);
            newGameObject.transform.SetParent(heartContainer);
        }

        if (IsReady())
        {
            for (int i = 0; i < heartContainer.childCount; i++)
            {
                GameObject childGameObject = heartContainer.GetChild(i).gameObject;

                if (i < _healthComponent.CurrentHealthPoints)
                {
                    childGameObject.SetActive(true);
                    childGameObject.GetComponent<Image>().sprite = fullHeartSprite;
                }
                else if (i < _healthComponent.MaxHealthPoints)
                {
                    childGameObject.SetActive(true);
                    childGameObject.GetComponent<Image>().sprite = emptyHeartSprite;
                }
                else
                {
                    childGameObject.SetActive(false);
                }
            }
        }
    }


    // public override void UpdateUI()
    // {
    //     if (IsReady())
    //     {
    //         float barMaxWidth = healthBarRectTransform.sizeDelta.x;
    //         float barHeight = healthBarRectTransform.sizeDelta.y;

    //         float healthPercentage = _healthComponent.GetHealthPercentage();
    //         float fillWidth = barMaxWidth * healthPercentage;

    //         fillImageRectTransform.sizeDelta = new Vector2(fillWidth, barHeight);
    //     }
    // }

    public bool IsReady()
    {
        return (_healthComponent != null);
    }
}