using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WinDiamond", menuName = "Custom Objects / WinDiamond")]
public class WinDiamond : Consumable
{
    protected override void Consume(GameObject targetGameObject)
    {
        LevelLoader.instance.AddGem(parentComponent.itemId);
        GameManager.instance.Win();
    }
}
