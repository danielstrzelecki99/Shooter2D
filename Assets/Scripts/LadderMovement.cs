using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
   private float vertical;
   private float speed = 8f;
   private bool isLadder;
   private bool isClimbing;

   [SerializeField] private Rigidbody2D rb;

    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
        Debug.Log("isClimbing" + isClimbing);
        Debug.Log("isLadder" + isLadder);
    }

    private void FixedUpdate() 
    {
        if(isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        else 
        {
            rb.gravityScale = 0.5f;
            speed = 2f;
        }    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}
