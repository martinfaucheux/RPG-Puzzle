using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Attack))]
public class Fighter : ActivableObject
{
    [SerializeField] bool _fightBack = true;

    private Health _health;
    private Attack _attack;
    private SpriteHolder _spriteHolder;

    protected override void Start()
    {
        base.Start();
        _health = GetComponent<Health>();
        _attack = GetComponent<Attack>();
        _spriteHolder = GetComponent<SpriteHolder>();

        if (_health == null)
        {
            Debug.LogError(gameObject.ToString() + ": no Health component found");
        }
    }

    public override bool CheckAllowMovement(GameObject sourceObject)
    {
        // fighter always blocks movement
        return false;
    }

    public override IEnumerator Activate(GameObject sourceObject)
    {
        if (sourceObject == null)
        {
            Debug.LogError(this.ToString() + ": No source Object given");
        }

        // get health and attack component of the source
        MatrixCollider opponentCollider = sourceObject.GetComponent<MatrixCollider>();
        Health opponentHealth = sourceObject.GetComponent<Health>();
        Attack opponentAttack = sourceObject.GetComponent<Attack>();

        if (
            opponentHealth != null
            && opponentAttack != null
            && !opponentAttack.isExhausted
        )
        {
            // opponent deals damage to this
            opponentAttack.Damage(_health);

            // wait for animation to complete
            yield return new WaitForSeconds(GameManager.instance.actionDuration);

            // if this is not dead, deal back damage
            if (!_health.isDead && _fightBack)
            {
                // face oponent 
                Direction directionToOpponent = _matrixCollider.GetDirectionToOtherCollider(opponentCollider);
                _spriteHolder.FaceDirection(directionToOpponent);

                _attack.Damage(opponentHealth);
                // wait for animation to complete
                yield return new WaitForSeconds(GameManager.instance.actionDuration);
            }
        }
        yield return null;
    }
}
