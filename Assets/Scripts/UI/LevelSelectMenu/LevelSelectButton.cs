using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComponent;
    [SerializeField] Image backgroundImage;
    [SerializeField] Image reflectionImage;

    private int _levelId;
    public int levelId
    {
        get { return _levelId; }
        set
        {
            _levelId = value;
            SetText();
        }
    }

    void Start()
    {
        levelId = transform.GetSiblingIndex() + 1;
    }

    private void SetText()
    {
        textComponent.text = levelId.ToString();
    }

    public void SelectLevel()
    {
        GetComponentInParent<LevelGridManager>().SelectLevel(levelId);
    }

    public void SetBackgroundColor(Color color)
    {
        backgroundImage.color = color;
    }

    public void SetReflectionColor(Color color)
    {
        reflectionImage.color = color;
    }



}
