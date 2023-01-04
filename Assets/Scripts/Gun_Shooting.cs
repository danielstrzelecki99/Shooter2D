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
    public Transform forearmHolder;
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

    WeaponManager weaponManager;

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


        weaponManager = GetComponent<WeaponManager>();
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
            //invoke methods to rotate gun and forearm
            FlipWeapon(gunHolder, 0);
            FlipWeapon(forearmHolder, weaponManager.CurrentWeaponNo);

            //react on left mouse button
            if (Input.GetMouseButton(0))
            {
                if (Time.time > ReadyForNextShoot)
                {
                    ReadyForNextShoot = Time.time + 1 / weaponController.fireRate;
                    weaponController.Shot();
                    //activate animation when fire button is pressed
                    shot = true;
                }
            }
            else
                shot = false;
            animator.SetBool("shoot", shot);
            //reload weapon when R button is pressed
            if (Input.GetKeyDown(KeyCode.R))
            {
                weaponController.Reload();
            }
            //switch reference to weapon when C button is pressed
            if (Input.GetKeyDown(KeyCode.C))
            {
                //load weapon settings 
                //weaponController = weapon.GetComponent<WeaponScript>();
                UpdateWeaponSettings();
                //reset rotation of the foreArm when switched to riffle
                //Debug.Log("Reseting forearm rotation");
                forearmHolder.transform.localRotation = Quaternion.identity;
            }
            if (ItemsManager.isAmmoPickup)
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

    private void FlipWeapon(Transform objectToRotate, int isRiffle)
    {
        if (isRiffle == 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - objectToRotate.position;
            float rotZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            objectToRotate.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            if (rotZ < 97 && rotZ > -95)
            {
                //Debug.Log("Facing right");
                objectToRotate.transform.Rotate(0f, 0f, objectToRotate.transform.rotation.z);
            }
            else
            {
                //Debug.Log("Facing left");
                objectToRotate.transform.Rotate(180f, 0f, objectToRotate.transform.rotation.z);
            }
        }
    }

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

    public void UpdateWeaponSettings()
    {
        weaponController = weapon.GetComponent<WeaponScript>();
    }
}