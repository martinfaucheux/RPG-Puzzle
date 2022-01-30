using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelObjectiveListElement : MonoBehaviour
{
    [SerializeField] Image checkBoxImage;
    [SerializeField] Color checkBoxCompleteColor;
    [SerializeField] Color checkBoxIncompleteColor;
    [SerializeField] TextMeshProUGUI questTextComponent;
    [SerializeField] Image vImage;
    private string questText;

    public void SetContent(string questText, bool isComplete, bool completeAnimation = false)
    {
        this.questText = questText;
        if (isComplete)
        {
            checkBoxImage.color = checkBoxCompleteColor;
            questTextComponent.text = "<s>" + questText + "</s>";
            if (vImage != null)
            {
                vImage.enabled = true;
                if (completeAnimation)
                {
                    DoCompleteAnimation();
                }
            }
        }
        else
        {
            checkBoxImage.color = checkBoxIncompleteColor;
            questTextComponent.text = questText;
            if (vImage != null)
            {
                vImage.enabled = false;
            }
        }
    }

    private void DoCompleteAnimation()
    {
        float transitionTime = 1f;
        float sizeMultiplier = 4f;

        RectTransform rectTransform = (RectTransform)vImage.transform;

        vImage.transform.localScale = sizeMultiplier * Vector3.one;
        Color color = vImage.color;
        color.a = 0;
        vImage.color = color;

        LeanTween.scale(rectTransform, Vector3.one, transitionTime).setEaseInQuart();
        LeanTween.alpha(rectTransform, 1f, transitionTime);
    }
}
