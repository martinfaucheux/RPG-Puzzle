using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private float attackAnimationAmplitude = 0.3f;

    public int attackPoints
    {
        get { return _attackPoints; }
        private set
        {
            _attackPoints = value;
            GameEvents.instance.AttackChangeTrigger(GetInstanceID());
        }
    }
    // is exhausted means that the unit already attacked during this turn
    public bool isExhausted { get; private set; } = false;
    [SerializeField] private int _attackPoints;
    public AttackAnimation attackAnimComponent;
    [SerializeField] string attackSoundName;
    private Experience _expComponent;
    private Health _healthComponent;
    private MatrixCollider _matrixCollider;
    private SpriteHolder _spriteHolder;

    private Animator _animator { get { return _spriteHolder!.activeAnimator; } }

    // Use this for initialization
    void Start()
    {
        _expComponent = GetComponent<Experience>();
        _healthComponent = GetComponent<Health>();
        _matrixCollider = GetComponent<MatrixCollider>();
        _spriteHolder = GetComponent<SpriteHolder>();

        if (_healthComponent == null)
        {
            Debug.LogWarning(gameObject + ": no Health component found");
        }
        GameEvents.instance.onTurnStart += RecoverExhaust;
    }

    void OnDestroy()
    {
        GameEvents.instance.onTurnStart -= RecoverExhaust;
    }

    private void RecoverExhaust() => isExhausted = false;

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

        // Play sound
        AudioManager.instance.Play(attackSoundName);

        opponentHealth.TakeDamage(attackPoints);

        if (opponentHealth.isDead)
        {
            if (_expComponent != null && !_healthComponent.isDead)
            {
                ExpBallSpawner.instance?.Spawn(transform.position);
                _expComponent.GainExp(opponentHealth.expRewardPoints);
            }
        }

        // mark as exhausted. It won't be able to damage before next turn
        isExhausted = true;
    }

    // animate the attack
    private void AnimateAttack(Direction direction = null)
    {
        if (_spriteHolder != null)
        {
            _spriteHolder.FaceDirection(direction);
        }
        if (_spriteHolder != null)
        {
            if (direction != null)
            {
                // if sprite holder is provided, the dash animation can be played
                float animationDuration = GameManager.instance.actionDuration;
                _spriteHolder.AttackMoveSprite(direction, attackAnimationAmplitude, animationDuration);
            }
            if (_animator != null && AnimatorUtils.HasParameter(_animator, "attack"))
                _animator.SetTrigger("attack");

        }

        // TODO: use pooling to lose reference to AttackAnimation component
        if (attackAnimComponent != null)
        {
            attackAnimComponent.Trigger(transform.position, direction);
        }
    }

    private void FaceOpponent(MatrixCollider opponentCollider)
    {
        Direction faceDirection = GetDirectionToOppenent(opponentCollider);
        if (faceDirection != null & _spriteHolder != null)
        {
            _spriteHolder.FaceDirection(faceDirection);
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

    private IEnumerator AnimateSlice(Direction direction)
    {
        GameObject attackEffectObject = GameObjectPool.instance.GetOrCreate("AttackAnimation");
        attackEffectObject.transform.position = transform.position;
        attackEffectObject.gameObject.SetActive(true);
        attackEffectObject.GetComponent<Animator>().SetTrigger("attack");

        yield return new WaitForSeconds(GameManager.instance.actionDuration * 0.99f);
        attackEffectObject.gameObject.SetActive(false);
    }
}
