using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class ChangeGemColor : MonoBehaviour
{
    public Color ghostColor;
    public PickableObject pickableObjectComponent;

    void Start()
    {
        if (isAlreadyCollected())
        {
            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.color = ghostColor;
        }
    }
    private bool isAlreadyCollected()
    {
        int currentLevelId = LevelLoader.instance.currentLevelId;
        int gemId = pickableObjectComponent.itemId;
        return SaveManager.instance.IsGemCollected(currentLevelId, gemId);
    }
}
