using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : ActivableObject {

    private Health _health;
    private Attack _attack;

	// Use this for initialization
	void Start () {
        _health = GetComponent<Health>();
        _attack = GetComponent<Attack>();

        if (_health == null)
        {
            Debug.LogError(gameObject.ToString() + ": no Health component found");
        }
    }

    public override bool Activate(GameObject sourceObject)
    {
        // bool opponentCanMove = true;

        if (sourceObject == null)
        {
            Debug.LogError(this.ToString() + ": No source Object given");
        }

        // get health and attack component of the source
        Health opponentHealth = sourceObject.GetComponent<Health>();
        Attack opponentAttack = sourceObject.GetComponent<Attack>();

        if (opponentHealth != null & opponentAttack != null)
        {
            _attack.Fight(opponentHealth, opponentAttack);
            // opponentCanMove = this._health.isDead & !opponentHealth.isDead;
        }

        return false;

    }


}
