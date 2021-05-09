using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHealthContainer : UIEntityComponent
{

    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;
    private Health _healthComponent;

    protected override void Start()
    {
        base.Start();
    }

    public override void AttachObject(GameObject attachedObject)
    {
        base.AttachObject(attachedObject);
        _healthComponent = attachedObject.GetComponent<Health>();
        UpdateUI();
    }

    // TODO: implement case of more than 3 health -> need to instantiate new images
    // don't forget to call SetHidableComponents or add them manually

    public override void UpdateUI()
    {
        // instantiate missing images
        int missingImageCount = _healthComponent.MaxHealthPoints - transform.childCount;
        for(int i = 0; i < missingImageCount; i++){
            Debug.Log("Instantiate " + i.ToString());
            GameObject newGameObject = Instantiate(transform.GetChild(0).gameObject);
            newGameObject.transform.SetParent(transform);
            MenuController.instance.RegisterHiddableComponent(newGameObject.GetComponent<Image>());

        }       

        if(IsReady()){
            for(int i = 0; i < transform.childCount; i++){
                GameObject childGameObject = transform.GetChild(i).gameObject;

                if (i < _healthComponent.CurrentHealthPoints){
                    childGameObject.SetActive(true);
                    childGameObject.GetComponent<Image>().sprite = fullHeartSprite;
                }
                else if (i < _healthComponent.MaxHealthPoints){
                    childGameObject.SetActive(true);
                    childGameObject.GetComponent<Image>().sprite = emptyHeartSprite;
                }
                else{
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