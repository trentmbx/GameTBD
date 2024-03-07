using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    public float walkSpeed = 5F;
    Vector2 moveInput;

    private bool _isMoving = false;

    // Property for getting the current move speed
    public float CurrentMoveSpeed { get
        {
            if (IsMoving)
            {
                return walkSpeed;
            }
            else
            {
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
            animator.SetBool("isMoving", value);
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

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        // Assign vars
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        // Update rigidbody velocity with walk speed
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Read input action context values
        moveInput = context.ReadValue<Vector2>();
        // Update whether character is moving or not
        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
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
}
