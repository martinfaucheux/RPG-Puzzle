using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WinDiamond", menuName = "Custom Objects / WinDiamond")]
public class WinDiamond : Consumable
{
    protected override void Consume(GameObject targetGameObject, int itemId)
    {
        LevelLoader.instance.AddGem(itemId);
        GameManager.instance.Win();
    }
}
