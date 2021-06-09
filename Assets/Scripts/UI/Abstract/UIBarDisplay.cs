using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIBarDisplay : MonoBehaviour
{
    public RectTransform imageContainerTransform; // most likely this.transform
    public GameObject defaultUIElementPrefab;
    private LayoutGroup _layoutGroup;

    // get the number of UIElements that should be displayed
    protected abstract int GetNewImageCount();

    // update the state of each UIElement
    protected abstract void UpdateElementStates();

    // Use this for initialization
    protected virtual void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        UpdateImageCount();
        UpdateElementStates();
    }

    private IEnumerator LateUpdateUI(float executeAfter = 0.05f)
    {
        yield return new WaitForSeconds(executeAfter);
        UpdateUI();
    }

    private void UpdateImageCount()
    {
        int newImageCount = GetNewImageCount();

        _layoutGroup = imageContainerTransform.GetComponent<LayoutGroup>();
        UIBarElement[] UIElementComponents = imageContainerTransform.GetComponentsInChildren<UIBarElement>();

        int imageCount;
        if (UIElementComponents == null)
        {
            imageCount = 0;
        }
        else
        {
            imageCount = UIElementComponents.Length;
        }

        // if there is not enough images, create the missing ones
        if (imageCount < newImageCount)
        {
            for (int i = 0; i < newImageCount - imageCount; i++)
            {
                // set parent is done at the end of frame to force re-calculate layout
                StartCoroutine(AddGameObject());
            }
        }

        // if there is too many images, hide the extra ones
        if (imageCount > newImageCount)
        {
            for (int i = newImageCount; i < imageCount; i++)
            {
                UIElementComponents[i].Hide();
            }
        }
    }

    private IEnumerator AddGameObject(){

        GameObject newUIElementObject = Instantiate(
            defaultUIElementPrefab,
            imageContainerTransform.position,
            Quaternion.identity,
            imageContainerTransform
        );
        RectTransform rectTransform = (RectTransform) newUIElementObject.transform;
        
        yield return new WaitForEndOfFrame();
        _layoutGroup.enabled = false;
        rectTransform.anchorMin = new Vector2(0.5f, 1);

        // NOTE: this might not work when adding 2 elements at the same time
        _layoutGroup.CalculateLayoutInputHorizontal();
        LayoutRebuilder.ForceRebuildLayoutImmediate(imageContainerTransform);
        _layoutGroup.enabled = true; // enable at the end of the operation
    }
}
