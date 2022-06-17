using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public Direction faceDirection { get; private set; } = Direction.IDLE;
    [Tooltip("Specify the initial direction to adopt")]
    [SerializeField] string _baseDirection;
    [SerializeField] string paceSoundName;

    protected MatrixCollider _matrixCollider;

    // the object is currently moving
    public bool isMoving { get; private set; } = false;

    // Needed to play the bump animation
    private SpriteHolder _spriteHolder;

    private Animator _animator
    {
        get
        {
            if (_spriteHolder != null)
                return _spriteHolder.activeAnimator;
            else
                return null;
        }
    }


    // Use this for initialization
    protected virtual void Start()
    {
        _matrixCollider = GetComponent<MatrixCollider>();
        if (_matrixCollider == null)
        {
            Debug.LogError(this.gameObject.ToString() + ": MatrixCollider not found");
        }
        _spriteHolder = GetComponent<SpriteHolder>();

        if (_baseDirection != "")
            faceDirection = Direction.FromString(_baseDirection);

        _spriteHolder.FaceDirection(faceDirection);
    }

    protected virtual IEnumerator Move(Direction direction)
    {
        faceDirection = direction;
        _spriteHolder.FaceDirection(direction);

        // update collider position
        _matrixCollider.matrixPosition += direction.ToPos();

        // update real position
        Vector3 realPosStart = transform.position;
        // need to use a CollisionMatrix method instead
        Vector3 realPosEnd = realPosStart + CollisionMatrix.instance.GetRealWorldVector(direction);

        // play sound
        if (paceSoundName.Length > 0)
        {
            AudioManager.instance?.Play(paceSoundName);
        }

        yield return StartCoroutine(SmoothMovement(realPosEnd));

        if (gameObject.tag == "Player")
        {
            GameEvents.instance.PlayerMoveTrigger(_matrixCollider.matrixPosition);
        }
    }

    protected IEnumerator SmoothMovement(Vector3 targetPos)
    {
        isMoving = true;
        if (_animator != null && AnimatorUtils.HasParameter(_animator, "bump"))
        {
            _animator.SetTrigger("bump");
        }
        LTDescr ltAnimation = LeanTween.move(gameObject, targetPos, GameManager.instance.actionDuration);
        while (LeanTween.isTweening(ltAnimation.id))
        {
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    public virtual IEnumerator AttemptMove(Direction direction)
    {
        // check movement is within matrix
        if (!_matrixCollider.IsValidDirection(direction))
            yield break;

        MatrixCollider collider = _matrixCollider.GetObjectInDirection(direction);
        ActivableObject activableObject = null;
        if (collider != null)
            activableObject = collider.GetComponent<ActivableObject>();

        // check if the interaction is valid
        // TODO: if not skip turn
        if (!IsInteractionAllowed(collider, activableObject))
            yield break;

        // if activable object allows movement
        if (activableObject == null || activableObject.CheckAllowMovement(gameObject))
        {
            LeavePosition(_matrixCollider.matrixPosition);
            yield return StartCoroutine(Move(direction));
        }

        if (activableObject != null)
        {
            yield return StartCoroutine(activableObject.Activate(gameObject));
        }
    }

    // <summary>
    // check that the direction is valid for interaction
    // if not, the turn should be skipped
    // </summary>
    private bool IsInteractionAllowed(MatrixCollider collider, ActivableObject activableObject)
    {
        if (activableObject != null)
            return activableObject.CheckAllowInteraction(gameObject);

        if (collider != null)
            return !collider.isBlocking;

        return true;
    }

    // <summary>
    // Should be used by other components
    // </summary>
    public bool IsInteractionAllowed(Direction direction)
    {
        // check movement is within matrix
        if (!_matrixCollider.IsValidDirection(direction))
            return false;

        MatrixCollider collider = _matrixCollider.GetObjectInDirection(direction);
        ActivableObject activableObject = null;
        if (collider != null)
            activableObject = collider.GetComponent<ActivableObject>();

        // check if the interaction is valid
        return IsInteractionAllowed(collider, activableObject);
    }

    // play OnLeave of current sitting Activable object
    private void LeavePosition(Vector2Int previousPosition)
    {

        List<MatrixCollider> collidersAtPosition = CollisionMatrix.instance.GetObjectsAtPosition(previousPosition);
        foreach (MatrixCollider matrixCollider in collidersAtPosition)
        {
            ActivableObject leavingActivableObject = matrixCollider.GetComponent<ActivableObject>();
            if (leavingActivableObject != null)
            {
                leavingActivableObject.OnLeave();
            }
        }
    }
}