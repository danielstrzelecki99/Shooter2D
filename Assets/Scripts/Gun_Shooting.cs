using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun_Shooting : MonoBehaviourPun
{
    public Transform gunHolder;
    private Animator animator;
    private bool shot = false;
    //Variable for flipping the player model
    private bool FacingRight = true; //For setting which way the player is facing
    public GameObject muzzle;
    public Transform nickName;
    public Transform healthBar;


    //Ammo variables
    public static int currentClip = 5, maxClipSize = 10, currentAmmo = 100, maxAmmoSize = 100;


    PhotonView view;
    public bool DisableInputs = false; //when player is dead variable disable inputs

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

            //activate animation fire button is pressed
            if (Input.GetMouseButton(0))
            {
                shot = true;
            }
            else
                shot = false;
            animator.SetBool("shoot", shot);
        }
        nickName.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // freeze rotation of nickname tag
        healthBar.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // freeze rotation of health bar
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
    [PunRPC]
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        FacingRight = !FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    //private void NicknameFlip()
    //{
    //    nickName.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    //}
}