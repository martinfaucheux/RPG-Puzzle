using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scroll : ActivableObject
{
    [SerializeField] Instruction _instructionObject;

    protected override void Start()
    {
        base.Start();
        if (_instructionObject == null)
            Debug.LogError("Scroll should have an Instruction object attached", this);
    }

    public override bool CheckAllowMovement(GameObject sourceObject) => false;
    public override bool CheckAllowInteraction(GameObject sourceObject)
    {
        return sourceObject.tag == "Player";
    }

    public override IEnumerator OnInteract(GameObject sourceObject)
    {
        Direction directionToOpponent = GetDirectionToOppenent(
            sourceObject.GetComponent<MatrixCollider>()
        );
        SpriteHolder spriteHolder = sourceObject.GetComponent<SpriteHolder>();
        spriteHolder.AttackMoveSprite(directionToOpponent);

        InstructionPanel.instance.Show(_instructionObject.description);
        yield return null;
    }

    private Direction GetDirectionToOppenent(MatrixCollider opponentCollider)
    {
        if (opponentCollider != null)
        {
            return _matrixCollider.GetDirectionToOtherCollider(opponentCollider).Opposite();
        }
        return null;
    }
}