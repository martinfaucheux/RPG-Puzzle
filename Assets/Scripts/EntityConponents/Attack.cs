﻿using System.Collections;
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

    public void AddAttackPoint(int attackPoint = 1)
    {
        // Increase the amount of damage dealt when attacking
        this.attackPoints += attackPoints;
    }

    public void Damage(Health opponentHealth)
    {
        // Main function called to apply damage to another Health component

        // get direction to opponent
        Direction directionToOpponent = GetDirectionToOppenent(
            opponentHealth.GetComponent<MatrixCollider>()
        );

        // face direction then animate attack
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
        if (_spriteHolder != null){
            _spriteHolder.FaceDirection(direction);
        }
        if (_animator != null)
        {
            _animator.SetTrigger("attack");
        }
        // if sprite holder is provided, the dash animation can be played
        if(_spriteHolder != null && direction != null) {
            float animationDuration = GameManager.instance.actionDuration;
            _spriteHolder.AttackMoveSprite(direction, attackAnimationAmplitude, animationDuration);
        }
        StartCoroutine(AnimateSlice(direction));
    }

    private void FaceOpponent(MatrixCollider opponentCollider)
    {
        Direction faceDirection = GetDirectionToOppenent(opponentCollider);
        if (faceDirection != null & _movingObject != null)
        {
            _movingObject.Face(faceDirection);
        }
    }

    private Direction GetDirectionToOppenent(MatrixCollider opponentCollider)
    {
        if (opponentCollider != null)
        {
            return _matrixCollider.GetDirectionToOtherCollider(opponentCollider);
        }
        return null;
    }

    // TODO: understand why the animation is not visible on the camera depending on point of view
    private IEnumerator AnimateSlice(Direction direction){
        GameObject attackEffectObject = ObjectPool.GetPool("attack").GetPooledObject();
        attackEffectObject.transform.position = transform.position;
        attackEffectObject.gameObject.SetActive(true);
        attackEffectObject.GetComponent<Animator>().SetTrigger("attack");

        yield return new WaitForSeconds(GameManager.instance.actionDuration * 0.99f);
        attackEffectObject.gameObject.SetActive(false);
    }
}
