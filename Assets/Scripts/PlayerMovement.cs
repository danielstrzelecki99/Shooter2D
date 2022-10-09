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
    private bool FacingRight = true; //For setting which way the player is facing
    float horizontalMove = 0f; //To define if player is moving

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

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (view.IsMine || !PhotonNetwork.InRoom)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (grounded || doubleJump)
                {
                    animator.SetBool("jump", Input.GetKey(KeyCode.Space));
                    Jump();
                }
            }
            if (Input.GetButtonDown("Crouch"))
            {
                if (grounded)
                {
                    speed /= 2;
                    animator.SetBool("crouch", true);
                }
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                speed *= 2;
                animator.SetBool("crouch", false);
            }
        }
        Physics2D.IgnoreLayerCollision(3, 3);
    }

    private void FixedUpdate()
    {
        if (view.IsMine || !PhotonNetwork.InRoom)
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
            if (horizontalInput > 0.01f && !FacingRight)
                Flip();
            else if (horizontalInput < -0.01f && FacingRight)
                Flip();

            //Set animator parameters
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
        if (doubleJump)
            animator.SetBool("jump", false);
        doubleJump = !doubleJump;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Ground")
        {
            grounded = true;
            Debug.Log(other.gameObject.tag);
        }
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        FacingRight = !FacingRight;
        transform.Rotate(0f, 180f, 0f);
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
