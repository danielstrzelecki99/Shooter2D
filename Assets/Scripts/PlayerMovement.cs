using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    //Move and jump
    private float horizontalInput;
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private bool grounded;
    private Animator animator;
    private bool doubleJump;

    //Dash
    // private bool canDash = true;
    // private bool isDashing;
    // private float dashingPower = 24f;
    // private float dashingTime = 0.2f;
    // private float dashingCooldown = 1f;
    //[SerializeField] private TrailRenderer tr;


    PhotonView view;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            // if (isDashing)
            // {
            //     return;
            // }

            if (grounded && !Input.GetKey(KeyCode.Space))
            {
                doubleJump = false;
            }
            
                        
            horizontalInput = Input.GetAxis("Horizontal");
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            //Flip player when moving left-right
            if (horizontalInput > 0.01f)
                transform.localScale = new Vector3((float)0.14, (float)0.14, (float)0.14);
            else if (horizontalInput < -0.01f)
                transform.localScale = new Vector3((float)-0.14, (float)0.14, (float)0.14);

            if(Input.GetButtonDown("Jump"))
            {
                if (grounded || doubleJump)
                {
                    animator.SetBool("jump", Input.GetKey(KeyCode.Space));
                    Jump();
                } 
            }

            //Set animator parameters
            animator.SetBool("run", horizontalInput != 0);
            animator.SetBool("grounded", grounded);
            animator.SetBool("jump", !grounded);
    
            //Set the yVelocity in the animator
            animator.SetFloat("yVelocity", body.velocity.y);
            // if (Input.GetKey(KeyCode.LeftShift) && canDash)
            // {
            //     StartCoroutine(Dash());
            // }
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        animator.SetBool("jump", true);
        grounded = false;
        doubleJump = !doubleJump;
        
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    // private IEnumerator Dash()
    // {
    //     canDash = false;
    //     isDashing = true;
    //     float originalGravity = body.gravityScale;
    //     body.gravityScale = 0f;
    //     if (horizontalInput > 0.01f)
    //     {
    //         body.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
    //     }
    //     else if (horizontalInput < -0.01f)
    //     {
    //         body.velocity = new Vector2(transform.localScale.x * -dashingPower, 0f);
    //     }
    //     //tr.emitting = true;
    //     yield return new WaitForSeconds(dashingTime);
    //     //tr.emitting = false;
    //     body.gravityScale = originalGravity;
    //     isDashing = false;
    //     yield return new WaitForSeconds(dashingCooldown);
    //     canDash = true;
    // }
}
