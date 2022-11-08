using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun.Demo.Asteroids;

public class Gun_Shooting : MonoBehaviourPun
{
    public Transform gunHolder;
    public GameObject Bullet;
    float ReadyForNextShoot;

    private Animator animator;
    private bool shot = false;
    //Variable for flipping the player model
    private bool FacingRight = true; //For setting which way the player is facing
    public GameObject muzzle;
    public Transform nickName;
    public Transform healthBar;


    //Ammo variables
    public int AcurrentClip, AmaxClipSize, AcurrentAmmo, AmaxAmmoSize;

    WeaponScript weaponController;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject riffle;
    WeaponScript riffleController;

    //ItemsManager itemsController;
    //[SerializeField] private GameObject items;

    PhotonView view;
    public bool DisableInputs = false; //when player is dead variable disable inputs

    private void Awake()
    {
        animator = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
        weaponController = weapon.GetComponent<WeaponScript>();
        riffleController = riffle.GetComponent<WeaponScript>();
        //itemsController = items.GetComponent<ItemsManager>();
    }
    // Update is called once per frame
    private void Update()
    {
        
        if (view.IsMine && !DisableInputs)
        {
            AcurrentClip = weaponController.currentClip;
            AmaxClipSize = weaponController.maxClipSize;
            AcurrentAmmo = weaponController.currentAmmo;
            AmaxAmmoSize = weaponController.maxAmmoSize;
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
                if (Time.time > ReadyForNextShoot)
                {
                    ReadyForNextShoot = Time.time + 1 / weaponController.fireRate;
                    weaponController.Shot();
                    shot = true;
                }
            }
            else
                shot = false;
            animator.SetBool("shoot", shot);
            if (Input.GetKeyDown(KeyCode.R))
            {
                weaponController.Reload();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                weaponController = weapon.GetComponent<WeaponScript>();
            }
            if(ItemsManager.isAmmoPickup)
            {
                riffleController.AddAmmo(20);
                ItemsManager.isAmmoPickup = false;
            }
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

    //public void Shot()
    //{
    //    if (weaponController.currentClip > 0)
    //    {
    //        //enable shoting animation
    //        shot = true;
    //        //Clone the bullet object every thime when shot funciton is involved
    //        PhotonNetwork.Instantiate(Bullet.name, new Vector2(firePoint.position.x, firePoint.position.y), firePoint.rotation, 0);
    //        weaponController.currentClip--;
    //        Debug.Log($"{weaponController.currentClip}/{weaponController.currentAmmo}");
    //    }
    //}
    //[PunRPC]
    //public void Reload()
    //{
    //    int reloadAmount = weaponController.maxClipSize - weaponController.currentClip; //how many bullets to refill cilp
    //    if (weaponController.currentAmmo - reloadAmount < 0)
    //        reloadAmount = weaponController.currentAmmo;
    //    weaponController.currentClip += reloadAmount;
    //    weaponController.currentAmmo -= reloadAmount;
    //    Debug.Log($"{weaponController.currentClip}/{weaponController.currentAmmo}");
    //}

    [PunRPC]
    private void Flip()
    {
        // Switch way the player is labelled as facing.
        FacingRight = !FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    //private void NicknameFlip()
    //{
    //    nickName.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    //}

    //Setters and getters
    public void SetWeapon(GameObject newWeapon)
    {
        weapon = newWeapon;
    }
}