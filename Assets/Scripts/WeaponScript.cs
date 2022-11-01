using Photon.Pun.Demo.Asteroids;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviourPun
{
    //Ammo variables
    public int currentClip, maxClipSize, currentAmmo, maxAmmoSize;
    public static int RcurrentClip, RcurrentAmmo;

    public GameObject Bullet;
    [SerializeField] public float fireRate;
    [SerializeField] private Transform firePoint;

    private void Update()
    {
        RcurrentClip = currentClip;
        RcurrentAmmo = currentAmmo;
    }

    public void Shot()
    {
        if (currentClip > 0)
        {
            //Clone the bullet object every thime when shot funciton is involved
            PhotonNetwork.Instantiate(Bullet.name, new Vector2(firePoint.position.x, firePoint.position.y), firePoint.rotation, 0);
            currentClip--;
            Debug.Log($"{currentClip}/{currentAmmo}");
        }
    }

    public void Reload()
    {
        int reloadAmount = maxClipSize - currentClip; //how many bullets to refill cilp
        if (currentAmmo - reloadAmount < 0)
            reloadAmount = currentAmmo;
        currentClip += reloadAmount;
        currentAmmo -= reloadAmount;
        Debug.Log($"{currentClip}/{currentAmmo}");
    }
    public void AddAmmo(int ammoAmount)
    {
        if (WeaponManager.CurrentWeaponNo != 0) //add ammo only for riffle
        {
            currentAmmo += ammoAmount;
            if (currentAmmo > maxAmmoSize)
            {
                currentAmmo = maxAmmoSize;
            }
        }
    }
}
