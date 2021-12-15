using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WinDiamond", menuName = "Custom Objects / WinDiamond")]
public class WinDiamond : Consumable
{
    protected override void Consume(GameObject targetGameObject)
    {
        // TODO: update this when gems have ids
        LevelLoader.instance.AddGem(0);
        GameManager.instance.Win();
    }
}
