using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    private float attackAnimationAmplitude = 0.3f;

    public int attackPoints;

    private Experience _expComponent;
    private Health _healthComponent;
    private Animator _animator;
    private MovingObject _movingObject;
    private MatrixCollider _matrixCollider;
    private SpriteHolder _spriteHolder;

    // Use this for initialization
    void Start () {
        _expComponent = GetComponent<Experience>();
        _healthComponent = GetComponent<Health>();
        _animator = GetComponent<Animator>();
        _movingObject = GetComponent<MovingObject>();
        _matrixCollider = GetComponent<MatrixCollider>();
        _spriteHolder = GetComponent<SpriteHolder>();

        if (_healthComponent == null)
        {
            Debug.Log(gameObject + ": no Health component found");
        }
    }

    public void AddAttack(int attackPoint = 1)
    {
        this.attackPoints += attackPoints;
    }

    public void Damage(Health opponentHealth)
    {
        // get direction to opponent
        Direction directionToOpponent = GetDirectionToOppenent(
            opponentHealth.GetComponent<MatrixCollider>()
        );

        AnimateAttack(directionToOpponent);
        opponentHealth.TakeDamage(attackPoints);

        if (opponentHealth.isDead)
        {
            if (_expComponent != null && !_healthComponent.isDead)
            {
                _expComponent.GainExp(opponentHealth.expRewardPoints);
            }
        } 
    }

    // damage each other with other entity
    public void Fight(Health opponentHealth, Attack opponentAttack)
    {
        CameraShake.instance.ShakeOnce();

        if(opponentHealth.gameObject.tag == "Player")
        {
            // Force this to face player
            FaceOpponent(opponentAttack._matrixCollider);

            // this attacks first
            this.Damage(opponentHealth);

            // opponent attacks then
            opponentAttack.Damage(this._healthComponent);
        }
        else
        {
            // oponents attacks first
            opponentAttack.Damage(this._healthComponent);

            // this attacks then
            this.Damage(opponentHealth);
        }
    }

    // animate the attack
    private void AnimateAttack(Direction direction = null)
    {
        if (_animator != null)
        {
            _animator.SetTrigger("attack");
        }
        // if sprite holder is provided, the dash animation can be played
        if(_spriteHolder != null && direction != null) {
            float animationDuration = MovingObject.moveTime;
            _spriteHolder.AttackMoveSprite(direction, attackAnimationAmplitude, animationDuration);
        }
    }

    private void FaceOpponent(MatrixCollider opponentCollider)
    {
        Direction faceDirection = GetDirectionToOppenent(opponentCollider);
        if (faceDirection != null)
        {
            _movingObject.Face(faceDirection);
        }
    }

    private Direction GetDirectionToOppenent(MatrixCollider opponentCollider)
    {
        if (_movingObject != null & opponentCollider != null)
        {
            return _matrixCollider.GetDirectionToOtherCollider(opponentCollider);
        }
        return null;
    }
}
