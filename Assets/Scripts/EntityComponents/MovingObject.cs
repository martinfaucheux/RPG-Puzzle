using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MovingObject : MonoBehaviour
{
    public LayerMask blockingLayer;
    [SerializeField] string paceSoundName;

    protected MatrixCollider _matrixCollider;
    private float _inverseMoveTime;
    private float _actionCoolDownTime;

    // the object can perform an action
    private bool _isReady = false;

    // the object is currently moving
    private bool _isMoving = false;

    private Animator _animator;

    // needed for face function
    private SpriteRenderer _spriteRenderer;
    private SpriteHolder _spriteHolder;



    // Use this for initialization
    protected virtual void Start()
    {
        _matrixCollider = GetComponent<MatrixCollider>();
        if (_matrixCollider == null)
        {
            Debug.LogError(this.gameObject.ToString() + ": MatrixCollider not found");
        }

        _inverseMoveTime = 1f / GameManager.instance.actionDuration;

        _animator = GetComponent<Animator>();
        _spriteHolder = GetComponent<SpriteHolder>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (!_isReady)
        {
            _actionCoolDownTime -= Time.deltaTime;
            if (_actionCoolDownTime < 0)
            {
                _isReady = true;
            }
        }
    }

    public bool IsReady()
    {
        return _isReady & !_isMoving;
    }

    private void SetNotReady()
    {
        _actionCoolDownTime = GameManager.instance.actionDuration;
        _isReady = false;
    }

    protected IEnumerator SmoothMovement(Vector3 endPos)
    {
        // sqr for the remaining distance
        // = distance between the current position and the end position
        float sqrRemainingDistance = (transform.position - endPos).sqrMagnitude;

        _isMoving = true;
        if (_spriteHolder != null & _spriteHolder.activeAnimator)
        {
            _spriteHolder.activeAnimator.SetTrigger("bump");
        }
        while (sqrRemainingDistance > float.Epsilon)
        {
            // move the rigidbody moveUnits units toward the end position
            float moveUnits = _inverseMoveTime * Time.deltaTime;
            Vector3 newPosition = Vector3.MoveTowards(transform.position, endPos, moveUnits);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - endPos).sqrMagnitude;
            // wait for a frame before reevalue the conditions of the loop
            yield return null;

        }

        // set at the correct place at the end
        transform.position = endPos;
        _isMoving = false;
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
    }

    public virtual IEnumerator AttemptMove(Direction direction)
    {
        // start cooldown for action
        SetNotReady();

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