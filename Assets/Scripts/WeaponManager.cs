using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private Animator animator;
    public static int CurrentWeaponNo;
    [SerializeField] Transform firePoint1;
    [SerializeField] Transform firePoint2;
    [SerializeField] GameObject weapon1;
    [SerializeField] GameObject weapon2;


    PhotonView view;
    public bool DisableInputs = false; //when player is dead variable disable inputs
    Gun_Shooting gunShootingScript;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gunShootingScript = GetComponent<Gun_Shooting>();
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
            }
        }
    }

    private void ChangeWeapon()
    {
        if (CurrentWeaponNo == 0) //riffle
        {
            CurrentWeaponNo += 1;
            animator.SetLayerWeight(CurrentWeaponNo - 1, 0);
            animator.SetLayerWeight(CurrentWeaponNo, 1);
            animator.SetBool("riffle", true);
            gunShootingScript.SetWeapon(weapon1);
            BulletProjectile.bulleteDamage = UnityEngine.Random.Range(.15f, .25f);
        }
        else //gun
        {
            CurrentWeaponNo -= 1;
            animator.SetLayerWeight(CurrentWeaponNo + 1, 0);
            animator.SetLayerWeight(CurrentWeaponNo, 1);
            animator.SetBool("riffle", false);
            gunShootingScript.SetWeapon(weapon2);
            BulletProjectile.bulleteDamage = UnityEngine.Random.Range(.1f, .15f);
        }

    }
}
