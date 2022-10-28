using Photon.Pun.Demo.Asteroids;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviourPun
{
    //Ammo variables
    public int currentClip, maxClipSize, currentAmmo, maxAmmoSize;
    public static int RcurrentClip, RmaxClipSize, RcurrentAmmo, RmaxAmmoSize;

    public GameObject Bullet;
    [SerializeField] private float fireRate;
    float ReadyForNextShoot;
    [SerializeField] private Transform firePoint;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time > ReadyForNextShoot)
            {
                ReadyForNextShoot = Time.time + 1 / fireRate;
                Shot();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        RcurrentClip = currentClip;
        RmaxClipSize = maxClipSize;
        RcurrentAmmo = currentAmmo;
        RmaxAmmoSize = maxAmmoSize;
    }

    [PunRPC]
    private void Shot()
    {
        if (currentClip > 0)
        {
            //enable shoting animation
            //shot = true;
            //Clone the bullet object every thime when shot funciton is involved
            PhotonNetwork.Instantiate(Bullet.name, new Vector2(firePoint.position.x, firePoint.position.y), firePoint.rotation, 0);
            currentClip--;
        }
    }

    public void Reload()
    {
        int reloadAmount = maxClipSize - currentClip; //how many bullets to refill cilp
        if (currentAmmo - reloadAmount < 0)
            reloadAmount = currentAmmo;
        currentClip += reloadAmount;
        currentAmmo -= reloadAmount;
    }
    public void AddAmmo(int ammoAmount)
    {
        currentAmmo += ammoAmount;
        if (currentAmmo > maxAmmoSize)
        {
            currentAmmo = maxAmmoSize;
        }
    }
}
