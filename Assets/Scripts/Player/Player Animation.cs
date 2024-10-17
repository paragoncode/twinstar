using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private movement movement;
    [SerializeField] private Collision collision;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Playerattack playerAttack;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.moveInput != 0)
        {
            animator.SetBool("isRunning", true);
        } else
        {
            animator.SetBool("isRunning", false);
        }

        if (movement.isPlayer1)
        {
            if (collision.onGround || collision.onPlayer2)
            {
                animator.SetBool("isGrounded", true);
            } else
            {
                animator.SetBool("isGrounded", false);
            }

        } else
        {
            if (collision.onGround || collision.onPlayer1)
            {
                animator.SetBool("isGrounded", true);
            } else
            {
                animator.SetBool("isGrounded", false);
            }
        }
        
        animator.SetFloat("velocityY", rb.velocity.y);

        animator.SetBool("isAttacking", playerAttack.attacking);

        if (movement.facingDirection == new Vector2(0,1))
        {
            animator.SetBool("isFacingUp", true);
        } else
        {            
            animator.SetBool("isFacingUp", false);
        }
    }

}
