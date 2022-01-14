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
    private string questText;

    public void SetContent(string questText, bool isComplete)
    {
        this.questText = questText;
        if (isComplete)
        {
            checkBoxImage.color = checkBoxCompleteColor;
            questTextComponent.text = "<s>" + questText + "</s>";
        }
        else
        {
            checkBoxImage.color = checkBoxIncompleteColor;
            questTextComponent.text = questText;
        }
    }
}
