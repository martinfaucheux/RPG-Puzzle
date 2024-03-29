﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovingObject : MonoBehaviour
{
    public Direction faceDirection { get; private set; } = Direction.IDLE;
    [Tooltip("Specify the initial direction to adopt")]
    [SerializeField] string _baseDirection;
    [SerializeField] string paceSoundName;
    [SerializeField] PaceAnimator _paceAnimator;
    protected MatrixCollider _matrixCollider;

    // the object is currently moving
    public bool isMoving { get; private set; } = false;

    // Needed to play the bump animation
    private SpriteHolder _spriteHolder;
    private AnimatorControllerParameter _animationParameter;

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


    void Start()
    {
        _matrixCollider = GetComponent<MatrixCollider>();
        if (_matrixCollider == null)
            Debug.LogError(this.gameObject.ToString() + ": MatrixCollider not found");

        _spriteHolder = GetComponent<SpriteHolder>();

        if (_baseDirection != "")
        {
            faceDirection = Direction.FromString(_baseDirection);
            _spriteHolder.FaceDirection(faceDirection);
        }
    }

    private IEnumerator Move(Direction direction)
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
        _paceAnimator.StartPace();

        LTDescr ltAnimation = LeanTween.move(gameObject, targetPos, GameManager.instance.actionDuration);
        while (LeanTween.isTweening(ltAnimation.id))
        {
            yield return null;
        }
        _paceAnimator.EndPace();

        transform.position = targetPos;
        isMoving = false;
    }

    public virtual IEnumerator AttemptMove(Direction direction)
    {
        // check movement is within matrix
        if (!_matrixCollider.IsValidDirection(direction))
            yield break;

        List<MatrixCollider> colliders = _matrixCollider.GetObjectsInDirection(direction);

        // check if the interaction is valid
        if (!IsInteractionAllowed(colliders))
            yield break;

        List<ActivableObject> sortedActivables = new List<ActivableObject>();
        foreach (MatrixCollider collider in colliders)
        {
            ActivableObject activableObject = collider.GetComponent<ActivableObject>();
            if (activableObject != null)
                sortedActivables.Add(activableObject);
        }
        sortedActivables = sortedActivables.OrderBy(a => a.interactionPriority).ToList();

        foreach (ActivableObject activableObject in sortedActivables)
            yield return StartCoroutine(activableObject.OnInteract(gameObject));

        if (sortedActivables.All(act => act.CheckAllowMovement(gameObject)))
        {
            // Leave position
            LeavePosition(_matrixCollider.matrixPosition);

            // Move object
            yield return StartCoroutine(Move(direction));

            // Enter activable object
            foreach (ActivableObject activableObject in sortedActivables)
                yield return StartCoroutine(activableObject.OnEnter(gameObject));
        }
    }

    // <summary>
    // check that the colliders and their activable object allow interaction
    // if not, the turn should be skipped
    // </summary>
    private bool IsInteractionAllowed(List<MatrixCollider> colliders)
    {
        if (!CollisionMatrix.instance.emptyAllowInteraction && colliders.Count == 0)
            return false;

        foreach (MatrixCollider collider in colliders)
        {
            if (!IsInteractionAllowed(collider))
                return false;
        }
        return true;
    }

    // <summary>
    // check that the collider and its activable object allow interaction
    // </summary>
    private bool IsInteractionAllowed(MatrixCollider collider)
    {
        ActivableObject activableObject = collider.GetComponent<ActivableObject>();
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

        return IsInteractionAllowed(_matrixCollider.GetObjectsInDirection(direction));
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