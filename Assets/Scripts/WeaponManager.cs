using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private Animator animator;
    int CurrentWeaponNo;
    public Transform firePoint1;
    public Transform firePoint2;

    PhotonView view;

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
        if (view.IsMine && Input.GetKeyDown(KeyCode.C))
        {
            ChangeWeapon();
        }
    }

    private void ChangeWeapon()
    {
        if (CurrentWeaponNo == 0)
        {
            CurrentWeaponNo += 1;
            animator.SetLayerWeight(CurrentWeaponNo - 1, 0);
            animator.SetLayerWeight(CurrentWeaponNo, 1);
            animator.SetBool("riffle", true);
            gunShootingScript.SetFirePoint(firePoint1);
            gunShootingScript.SetFireRate(4);
            BulletProjectile.bulleteDamage = 0.3f;
        }
        else
        {
            CurrentWeaponNo -= 1;
            animator.SetLayerWeight(CurrentWeaponNo + 1, 0);
            animator.SetLayerWeight(CurrentWeaponNo, 1);
            animator.SetBool("riffle", false);
            gunShootingScript.SetFirePoint(firePoint2);
            gunShootingScript.SetFireRate(2);
            BulletProjectile.bulleteDamage = 0.15f;
        }

    }
}
