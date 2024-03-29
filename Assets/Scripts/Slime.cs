using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class Slime : MonoBehaviour
{
    public float walkSpeed = 3f;
    public DetectionZone attackZone;
    public float walkStopRate = 0.6f;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    SpriteRenderer spriteRenderer;
    Animator animator;

    public enum WalkableDirection { Right, Left }

    private WalkableDirection _walkDirection = WalkableDirection.Right;
    private Vector2 walkDirectionVector;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set { 
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                } else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }

            _walkDirection = value; }
    }

    public bool _hasTarget = false;

    public bool HasTarget { get { return _hasTarget; } private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
            // bad work around here, but works for the time being.
            spriteRenderer.flipX = true;
        }
        if (CanMove)
        {
            rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
        } else
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }
        
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        } else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        } else
        {
            Debug.LogError("Current walkable direction is not set to legal values.");
        }
    }
}
