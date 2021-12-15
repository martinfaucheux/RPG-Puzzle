using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComponent;

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

}
