using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComponent;

    private int levelId;

    void Start()
    {
        levelId = transform.GetSiblingIndex() + 1;
        textComponent.text = levelId.ToString();
    }

    public void SelectLevel()
    {
        GetComponentInParent<LevelGridManager>().SelectLevel(levelId);
    }

}
