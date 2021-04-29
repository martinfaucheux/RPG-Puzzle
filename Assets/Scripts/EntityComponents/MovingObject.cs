using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MovingObject : MonoBehaviour
{
    public LayerMask blockingLayer;
    
    private MatrixCollider _matrixCollider;
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
        if (!_isReady) {
            _actionCoolDownTime -= Time.deltaTime;
            if (_actionCoolDownTime< 0){
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
        AnimateWalk(true);
        if(_spriteHolder != null & _spriteHolder.activeAnimator){
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
        AnimateWalk(false);
    }

    protected bool Move(Direction direction)
    {
        Face(direction);

        // update collider position
        _matrixCollider.matrixPosition += direction.ToPos();

        // update real position
        Vector3 realPosStart = transform.position;
        // need to use a CollisionMatrix method instead
        Vector3 realPosEnd = realPosStart + CollisionMatrix.instance.GetRealWorldVector(direction);

        StartCoroutine(SmoothMovement(realPosEnd));

        // return True if we successfuly move
        return true;
    }

    protected virtual void AttemptMove(Direction direction)
    {
        // start cooldown for action
        SetNotReady();

        GameObject collidingObject = _matrixCollider.GetObjectInDirection(direction);
        bool canMove = true;

        if (collidingObject != null)
        {
            MatrixCollider otherCollider = collidingObject.GetComponent<MatrixCollider>();
            ActivableObject activableObject = collidingObject.GetComponent<ActivableObject>();
            canMove = !otherCollider.IsBlocking;

            if (otherCollider == null)
            {
                Debug.LogError(otherCollider.ToString() + ": colliding but no collider found");
                return;
            }

            if (activableObject != null)
            {
                canMove = activableObject.Activate(gameObject);
            }
        }        

        // Check that direction is valid and that object is able to move
        if (_matrixCollider.IsValidDirection(direction) & canMove)
        {
            LeavePosition(_matrixCollider.matrixPosition);
            Move(direction);
        }
    }

    private void AnimateWalk(bool start = true)
    {
        // TODO: to be deprecated
        if (_animator != null)
        {
            if (start) {
                _animator.SetBool("walk", true);
            }
            else
            {
                StartCoroutine(
                    CheckAnimatorMoving(GameManager.instance.actionDuration / 10f)
                );
            }
        }
    }

    // play OnLeave of current sitting Activable object
    private void LeavePosition(Vector2Int previousPosition){

        List<GameObject> objectsAtPosition = CollisionMatrix.instance.GetObjectsAtPosition(previousPosition);
        foreach(GameObject gameObject in objectsAtPosition){
            ActivableObject leavingActivableObject = gameObject.GetComponent<ActivableObject>();
            if (leavingActivableObject != null){
                leavingActivableObject.OnLeave();
            }
        }        
    }

    protected IEnumerator CheckAnimatorMoving(float totalWaitTime)
    {

        float waitTime = 0;

        while (waitTime < totalWaitTime)
        {
            waitTime += Time.deltaTime;
            yield return null;
        }

        if (!_isMoving)
        {
            _animator.SetBool("walk", false);
        }
    }

    public void Face(Direction direction)
    {
        if (_spriteHolder != null)
        {
            _spriteHolder.FaceDirection(direction);
        }
        else if (_spriteRenderer != null)
        {
            if (direction == Direction.RIGHT)
            {
                _spriteRenderer.flipX = false;
            }
            else if (direction == Direction.LEFT)
            {
                _spriteRenderer.flipX = true;
            }
        }
    }
}