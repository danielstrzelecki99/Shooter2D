using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private Animator animator;
    [SerializeField] public int CurrentWeaponNo;
    public static int CurrentWeaponNoForGameManager = 0;
    [SerializeField] Transform firePoint1;
    [SerializeField] Transform firePoint2;
    [SerializeField] private GameObject weapon1;
    [SerializeField] private GameObject weapon2;


    PhotonView view;
    public bool DisableInputs = false; //when player is dead variable disable inputs
    Gun_Shooting gunShootingScript;
    [SerializeField] private GameObject bullet;
    BulletProjectile bulletController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gunShootingScript = GetComponent<Gun_Shooting>();
        bulletController = bullet.GetComponent<BulletProjectile>();
    }
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine && !DisableInputs)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                ChangeWeapon();
                //Debug.Log($"Current weapon no: {CurrentWeaponNo}");
            }
        }
    }
    [PunRPC]
    public void ChangeWeapon()
    {
        if (CurrentWeaponNo == 0) //change from gun --> riffle
        {
            CurrentWeaponNo += 1;
            CurrentWeaponNoForGameManager += 1;
            animator.SetLayerWeight(CurrentWeaponNo - 1, 0);
            animator.SetLayerWeight(CurrentWeaponNo, 1);
            animator.SetBool("riffle", true);
            //change ammo, fire point, fire rate to riffle
            gunShootingScript.SetWeapon(weapon1);
            bulletController.minDmg = 0.19f;
            bulletController.maxDmg = 0.24f;
        }
        else //change from riffle --> gun 
        {
            CurrentWeaponNo -= 1;
            CurrentWeaponNoForGameManager -= 1;
            animator.SetLayerWeight(CurrentWeaponNo + 1, 0);
            animator.SetLayerWeight(CurrentWeaponNo, 1);
            animator.SetBool("riffle", false);
            //change ammo, fire point, fire rate to gun
            gunShootingScript.SetWeapon(weapon2);
            bulletController.minDmg = 0.13f;
            bulletController.maxDmg = 0.18f;
        }
    }
}
