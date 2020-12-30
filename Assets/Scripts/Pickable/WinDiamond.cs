using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WinDiamond", menuName = "Custom Objects / WinDiamond")]
public class WinDiamond : Consumable
{


    public override void Consume(GameObject targetGameObject)
    {
        GameManager.instance.Win();
    }
}
