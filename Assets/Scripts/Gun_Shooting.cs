using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject impactEffect;
    private Animator animator;
    private bool shot = false;
    public LineRenderer lineRenderer;
    [SerializeField] private float radius;
    //Variable for flipping the player model
    private bool FacingRight = true; //For setting which way the player is facing


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    private void Update()
    {
        //make action when fire button is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Shoot()); //StartCoroutine because WaitForSeconds to disable line
        }
        else
            shot = false;
        animator.SetBool("shot", shot);
    }

    private void FixedUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x > transform.position.x && !FacingRight)
        {
            Flip();
        }
        else if (mousePos.x < transform.position.x && FacingRight)
        {
            Flip();
        }
    }

    IEnumerator Shoot()
    {
        //send ray from the certain point and direction
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);
        shot = true;
        if (hitInfo) //if bullet hit the target
        {
            Debug.Log(hitInfo.transform.name);
            Instantiate(impactEffect, hitInfo.point, firePoint.transform.rotation);
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);
        }

        lineRenderer.enabled = true;
        // waiting to disable line
        yield return new WaitForSeconds(0.02f);
        lineRenderer.enabled = false;
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        FacingRight = !FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
