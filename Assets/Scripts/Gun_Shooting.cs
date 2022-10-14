using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Shooting : MonoBehaviour
{
    public Transform gunHolder;
    [SerializeField] private Transform firePoint;
    private Animator animator;
    private bool shot = false;
    //Variable for flipping the player model
    private bool FacingRight = true; //For setting which way the player is facing

    //Bullet variables
    public GameObject Bullet;
    [SerializeField] private float BulletSpeed;
    [SerializeField] private float fireRate;
    float ReadyForNextShoot;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    private void Update()
    {
        //rotate gun towards mousePosition
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gunHolder.position;
        float rotZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        gunHolder.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        if (rotZ < 97 && rotZ > -89)
        {
            Debug.Log("Facing right");
            gunHolder.transform.Rotate(0f, 0f, gunHolder.transform.rotation.z);
        }
        else
        {
            Debug.Log("Facing left");
            gunHolder.transform.Rotate(180f, 0f, gunHolder.transform.rotation.z);
        }

        //make action when fire button is pressed
        if (Input.GetMouseButton(0))
        {
            if(Time.time > ReadyForNextShoot)
            {
                ReadyForNextShoot = Time.time + 1 / fireRate;
                Shoot();
            }
        }
        else
            shot = false;
        animator.SetBool("shoot", shot);
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

    void Shoot()
    {
        shot = true;
        GameObject BulletIns = Instantiate(Bullet, firePoint.position, firePoint.rotation);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(BulletIns.transform.right * BulletSpeed);
        //animator.SetTrigger("shoot");
        Destroy(BulletIns, 3);
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        FacingRight = !FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void SetFirePoint(Transform newFirePoint)
    {
        firePoint = newFirePoint;
    }
    public void SetBulletSpeed(float newBulletspeed)
    {
        BulletSpeed = newBulletspeed;
    }
    public void SetFireRate(float newFirRate)
    {
        fireRate = newFirRate;
    }
}
