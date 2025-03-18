using System;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]

public class PlayerController : MonoBehaviour
{

    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    private float jumpImpulse = 9f;

      private float doubleJumpImpulse = 5f;

    public int numSaltos = 0;
    public int numSaltosMaximos = 1;


    TouchingDirections touchingDirections;


    public float CurrentMoveSpeed
    {
        get
        {
            if (IsMoving && !touchingDirections.IsOnWall)
            {
                if (IsRunning)
                {
                    return runSpeed;

                }
                else
                {
                    return walkSpeed;
                }
            }
            else
            {
                return 0;
            }
        }
    }





    Vector2 moveInput;
    [SerializeField]
    private bool _isMoving = false;



    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    [SerializeField]
    private bool _isRunning = false;
    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }


    public bool _isFacingRigth = true;
    public bool IsFacingRigth
    {
        get { return _isFacingRigth; }
        private set
        {
            if (_isFacingRigth != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRigth = value;
        }
    }

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }




    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.linearVelocityY);

        animator.SetFloat(AnimationStrings.yVelocity, rb.linearVelocityY);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);


    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRigth)
        {
            IsFacingRigth = true;
        }
        else if (moveInput.x < 0 && IsFacingRigth)
        {
            IsFacingRigth = false;
        }



    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }



    }

 public void OnJump(InputAction.CallbackContext context)
{
    if (context.started)
    {
        if (touchingDirections.IsGrounded || Time.time - touchingDirections.lastGroundedTime <= touchingDirections.coyoteTime)
        {
            // Salto desde el suelo
            Jump(jumpImpulse);
            numSaltos = 1;
        }
        else if (numSaltos < numSaltosMaximos)
        {
            // Salto en el aire (doble salto)
              Jump(doubleJumpImpulse);
            numSaltos = 2;
           
        }
    }
}

    
private void Jump(float impulso)
{
    animator.SetTrigger(AnimationStrings.jump);
    rb.linearVelocity = new Vector2(rb.linearVelocityX, impulso);
}
}
