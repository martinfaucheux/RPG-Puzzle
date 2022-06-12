using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHealthContainer : UIEntityComponent
{

    [SerializeField] Sprite _fullHeartSprite;
    [SerializeField] Sprite _emptyHeartSprite;
    [SerializeField] Transform heartContainer;

    protected override bool ShouldDisplay(InspectorData inspectorData)
    {
        return inspectorData.health != null;
    }

    protected override void UpdateUI(InspectorData inspectorData)
    {
        // instantiate missing images
        int missingImageCount = inspectorData.health.MaxHealthPoints - heartContainer.childCount;
        for (int i = 0; i < missingImageCount; i++)
        {
            GameObject newGameObject = Instantiate(heartContainer.GetChild(0).gameObject);
            newGameObject.transform.SetParent(heartContainer);
        }

        for (int i = 0; i < heartContainer.childCount; i++)
        {
            GameObject childGameObject = heartContainer.GetChild(i).gameObject;

            if (i < inspectorData.health.CurrentHealthPoints)
            {
                childGameObject.SetActive(true);
                childGameObject.GetComponent<Image>().sprite = _fullHeartSprite;
            }
            else if (i < inspectorData.health.MaxHealthPoints)
            {
                childGameObject.SetActive(true);
                childGameObject.GetComponent<Image>().sprite = _emptyHeartSprite;
            }
            else
            {
                childGameObject.SetActive(false);
            }
        }
    }
}