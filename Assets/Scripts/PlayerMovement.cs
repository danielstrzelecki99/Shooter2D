using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    //Move and jump
    private float horizontalInput;
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private bool grounded;
    private Animator animator;
    private bool doubleJump;
    private bool crouch = false;
    float horizontalMove = 0f; //To define if player is moving

    //Interact with items on map variables
    public static string interactInfoText;
    public static bool pickUpAllowed = false;
    public AudioSource src;
    public AudioClip jumpSound;

    PhotonView view;

    public bool DisableInputs = false; //when player is dead variable disable inputs

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        if (view.IsMine) { GameManagerScript.instance.LocalPlayer = gameObject; }
        
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public override void OnEnable()
    {
        speed = 4;
    }

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(6,6);
    }

    private void Update()
    {
        if (view.IsMine && !DisableInputs)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            if (Input.GetButtonDown("Jump"))
            {
                if (grounded || doubleJump)
                {
                    animator.SetBool("jump", Input.GetKey(KeyCode.Space));
                    Jump();
                }
            }
            if (grounded)
            {
                if (Input.GetButtonDown("Crouch"))
                {
                    crouch = true;
                    speed /= 2;
                    animator.SetBool("crouch", crouch);
                }
            }
            if (Input.GetButtonUp("Crouch") && crouch)
            {
                crouch = false;
                speed *= 2;
                animator.SetBool("crouch", crouch);
            }
        }
    }

    private void FixedUpdate()
    {
        if ((view.IsMine || !PhotonNetwork.InRoom) && !DisableInputs)
        {
            if (grounded && !Input.GetKey(KeyCode.Space))
            {
                doubleJump = false;
            }
                 
            horizontalInput = Input.GetAxis("Horizontal");
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            //Set animator parameters
            animator.SetBool("grounded", grounded);
            animator.SetBool("jump", !grounded);
    
            //Set the yVelocity in the animator
            animator.SetFloat("yVelocity", body.velocity.y);
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        animator.SetBool("jump", true);
        grounded = false;
        if (doubleJump)
        {
            animator.SetBool("jump", false);
        }
        doubleJump = !doubleJump;
        src.clip = jumpSound;
        src.Play();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    //Interact with items on map methods
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "AmmoKit" || other.gameObject.tag == "Armor" || other.gameObject.tag == "AidKit"){
            if (view.IsMine)
            {
                interactInfoText = "Press [E] to take item!";
                pickUpAllowed = true;
            }
            ItemsManager.selectedObject = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "AmmoKit" || other.gameObject.tag == "Armor" || other.gameObject.tag == "AidKit"){
            interactInfoText = "";
            pickUpAllowed = false;
        }
    }
}
