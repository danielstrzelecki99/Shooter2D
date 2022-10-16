using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class ButterflyMove : MonoBehaviour
{

    private Rigidbody2D body;

    [SerializeField]  float moveSpeed = 1f;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        body.velocity = new Vector2(moveSpeed, body.velocity.y);

    }

    void flip(){
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        moveSpeed *= -1;
    }
}
