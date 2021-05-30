using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIBarDisplay : MonoBehaviour
{
    public Transform imageContainerTransform;
    public GameObject defaultUIElementPrefab;

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
                Debug.Log("instantiate new");
                GameObject newUIElementObject = Instantiate(
                    defaultUIElementPrefab,
                    imageContainerTransform.transform.position,
                    Quaternion.identity
                );
                // set parent is done at the end of frame to force re-calculate layout
                StartCoroutine(SetTransformParent(newUIElementObject));
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

    private IEnumerator SetTransformParent(GameObject newGameObject){
        yield return new WaitForEndOfFrame();
        RectTransform rectTransform = (RectTransform) newGameObject.transform;
        rectTransform.SetParent(imageContainerTransform);
    }
}
