using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{

    public float walkSpeed = 5F;
    public float jumpImpulse = 10f;
    public float airWalkSpeed = 4f;
    Vector2 moveInput;

    private bool _isMoving = false;
    TouchingDirections touchingDirections;

    // Property for getting the current move speed
    public float CurrentMoveSpeed { get
        {
            if (CanMove) 
            {
            if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        return walkSpeed;
                    }
                    else
                    {
                        //Air move
                        return airWalkSpeed;
                    }
                }
            else
            {
                return 0;
            }
            } else
            {
                //Movement locked
                return 0;
            }

        }
        
    }

    // Property for whether character is moving (also updates animator moving value)
    public bool IsMoving { get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        } 
    }

    public bool _isFacingRight = true;

    // Property checking direction character is facing
    public bool IsFacingRight { get { return _isFacingRight; } private set{
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        }
    }

    public bool CanMove { get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    public bool LockVelocity { get {
            return animator.GetBool(AnimationStrings.lockVelocity);
        } }

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        // Assign vars
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (!LockVelocity)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        // Read input action context values
        moveInput = context.ReadValue<Vector2>();

        if(IsAlive)
        {
            // Update whether character is moving or not
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        } else {
            IsMoving = false;
        }

        
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            // face right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            // face left
            IsFacingRight = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // TODO check if alive
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attack);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
