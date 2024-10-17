using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] private bool isPlayer1;
    [SerializeField] private KeyCode leftInput;
    [SerializeField] private KeyCode rightInput;
    [SerializeField] private KeyCode upInput;
    
    [SerializeField] private KeyCode downInput;

    [Header("everything else")]

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collision collision;
    [SerializeField] private float moveSpeed = 10;
    private float moveInput = 0;
    [SerializeField] private float accelerationRate;
    [SerializeField] private float acceleration = 4;
    [SerializeField] private float decceleration = 4;
    [SerializeField] private float jumpForce = 15;
    [SerializeField] private float defaultGravity = 4;
    [SerializeField] private float fastFallGravity = 8;
    private bool doFastFall = false;
    private bool canJump;
    private bool doJump = false;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float coyoteTimeCounter;
    private bool isFacingRight;
    public UnityEngine.Vector2 facingDirection;
    
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(leftInput))
        {
            moveInput = -1;
            isFacingRight = false;
        } else if (Input.GetKey(rightInput))
        {
            moveInput = 1;
            isFacingRight = true;
        } else
        {
            moveInput = 0;
        }

        if (isPlayer1)
        {
            if (collision.onGround || collision.onPlayer2)
            {
                canJump = true;
            } else
            {
                canJump = false;
            }
        } else
        {
            if (collision.onGround || collision.onPlayer1)
            {
                canJump = true;
            } else
            {
                canJump = false;
            }
        }
        
        if (canJump)
        {
            coyoteTimeCounter = coyoteTime;
        }else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(upInput) && coyoteTimeCounter > 0)
        {
            doJump = true;
        }

        if (Input.GetKeyUp(upInput) || rb.velocity.y < -5)
        {
            doFastFall = true;
        }
        
        HandleOrientation();
    }
    private void FixedUpdate() 
    {        
        Walk();
        
        if (doJump)
        {
            Jump();
        }
        
        HandleGravity();
    }

    private void HandleOrientation()
    {
        if (Input.GetKey(upInput))
        {
            facingDirection = new UnityEngine.Vector2(0,1);
        } else if (Input.GetKey(downInput) && !collision.onGround)
        {
            facingDirection = new UnityEngine.Vector2(0,-1);
        } else if (isFacingRight)
        {
            facingDirection = new UnityEngine.Vector2(1,0);
        } else
        {
             facingDirection = new UnityEngine.Vector2(-1,0);
        }
    }
    private void HandleGravity()
    {
        if (doFastFall)
        {
            doFastFall = false;
            rb.gravityScale = fastFallGravity;
            coyoteTimeCounter = 0;
        } else if (canJump)
        {
            rb.gravityScale = defaultGravity;
        }
    }
    private void Jump() 
    {        
        doJump = false;
        rb.AddForce(UnityEngine.Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Walk()
    {
        float targetSpeed = moveInput * moveSpeed;
        float speedDifference = targetSpeed - rb.velocity.x;

        if (Mathf.Abs(targetSpeed) > 0 && Mathf.Abs(targetSpeed) > Mathf.Abs(rb.velocity.x)) 
        {
            if (collision.onGround)
            {
            accelerationRate = acceleration;
            } else
            {
                accelerationRate = acceleration / 2;
            }
        } else
        {
            if (collision.onGround)
            {
            accelerationRate = decceleration;
            } else
            {
                accelerationRate = decceleration / 10;
            }
        }
        float movement = speedDifference * accelerationRate;

        rb.AddForce(movement * UnityEngine.Vector2.right);
    }
}
