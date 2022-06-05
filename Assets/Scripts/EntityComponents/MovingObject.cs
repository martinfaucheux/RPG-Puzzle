using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] string paceSoundName;

    protected MatrixCollider _matrixCollider;

    // the object is currently moving
    public bool isMoving { get; private set; } = false;

    // Needed to play the bump animation
    private SpriteHolder _spriteHolder;

    // Use this for initialization
    protected virtual void Start()
    {
        _matrixCollider = GetComponent<MatrixCollider>();
        if (_matrixCollider == null)
        {
            Debug.LogError(this.gameObject.ToString() + ": MatrixCollider not found");
        }
        _spriteHolder = GetComponent<SpriteHolder>();
    }



    protected virtual IEnumerator Move(Direction direction)
    {
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
        if (_spriteHolder != null & _spriteHolder.activeAnimator)
        {
            _spriteHolder.activeAnimator.SetTrigger("bump");
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
        MatrixCollider otherCollider = _matrixCollider.GetObjectInDirection(direction);
        bool canMove = true;

        if (otherCollider != null)
        {
            ActivableObject activableObject = otherCollider.GetComponent<ActivableObject>();
            canMove = !otherCollider.IsBlocking;

            if (activableObject != null)
            {
                yield return StartCoroutine(activableObject.Activate(gameObject));
                canMove = activableObject.allowMovement;
            }
        }

        // Check that direction is valid and that object is able to move
        if (_matrixCollider.IsValidDirection(direction) & canMove)
        {
            LeavePosition(_matrixCollider.matrixPosition);
            yield return StartCoroutine(Move(direction));
        }
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