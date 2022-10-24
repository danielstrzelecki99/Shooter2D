using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun_Shooting : MonoBehaviourPun
{
    public Transform gunHolder;
    [SerializeField] private Transform firePoint;
    private Animator animator;
    private bool shot = false;
    //Variable for flipping the player model
    private bool FacingRight = true; //For setting which way the player is facing
    public GameObject muzzle;
    public Transform nickName;

    //Bullet variables
    public GameObject Bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float fireRate;
    float ReadyForNextShoot;

    PhotonView view;
    //when player is dead variable disable inputs
    public bool DisableInputs = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
    }
    // Update is called once per frame
    private void Update()
    {
        if ((view.IsMine || !PhotonNetwork.InRoom) && !DisableInputs)
        {
            //rotate gun towards mousePosition
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gunHolder.position;
            float rotZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            gunHolder.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            if (rotZ < 97 && rotZ > -89)
            {
                //Debug.Log("Facing right");
                gunHolder.transform.Rotate(0f, 0f, gunHolder.transform.rotation.z);
            }
            else
            {
                //Debug.Log("Facing left");
                gunHolder.transform.Rotate(180f, 0f, gunHolder.transform.rotation.z);
            }

            //make action when fire button is pressed
            if (Input.GetMouseButton(0))
            {
                if (Time.time > ReadyForNextShoot)
                {
                    ReadyForNextShoot = Time.time + 1 / fireRate;
                    Shoot();
                }
            }
            else
                shot = false;
            animator.SetBool("shoot", shot);
        }
    }

    private void FixedUpdate()
    {
        if ((view.IsMine || !PhotonNetwork.InRoom) && !DisableInputs)
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
    }

    void Shoot()
    {
        shot = true;
        //Edit bullet speed depends on weapon
        GameObject BulletIns = PhotonNetwork.Instantiate(Bullet.name, firePoint.position, firePoint.rotation, 0);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(BulletIns.transform.right * bulletSpeed);
        //animator.SetTrigger("shoot");
    }
    [PunRPC]
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        FacingRight = !FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    //private void NicknameFlip(){

      //  nickName.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    //}

    //Setters and getters
    public void SetFirePoint(Transform newFirePoint)
    {
        firePoint = newFirePoint;
    }
    public void SetBulletSpeed(float newBulletspeed)
    {
        bulletSpeed = newBulletspeed;
    }
    public void SetFireRate(float newFirRate)
    {
        fireRate = newFirRate;
    }
}
