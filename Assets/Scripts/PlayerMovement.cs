using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private bool grounded;
    private bool falling;
    private Animator animat;

    private void Awake()
    {

        body = GetComponent<Rigidbody2D>();
        animat = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3((float)0.14, (float)0.14, (float)0.14);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3((float)-0.14, (float)0.14, (float)0.14);

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.Space))
            falling = true;
            animat.SetBool("fall", falling);

        //Set animator parameters
        animat.SetBool("run", horizontalInput != 0);
        animat.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;
        falling = false;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
