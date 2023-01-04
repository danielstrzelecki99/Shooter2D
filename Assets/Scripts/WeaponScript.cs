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

    public ParticleSystem muzzleFlash;

    PhotonView view;
    public AudioSource src;
    public AudioClip shotSound, reloadSound;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if(view.IsMine)
        {
            RcurrentClip = currentClip;
            RcurrentAmmo = currentAmmo;
        }
    }

    public void Shot()
    {
        if (currentClip > 0)
        {
            //create muzzle effect
            muzzleFlash.Play();
            //Clone the bullet object every time when shot funciton is involved
            PhotonNetwork.Instantiate(Bullet.name, new Vector2(firePoint.position.x, firePoint.position.y), firePoint.rotation, 0);
            src.clip = shotSound;
            src.Play();
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
        RcurrentClip = currentClip;
        RcurrentAmmo = currentAmmo;
        src.clip = reloadSound;
        src.Play();
        //Debug.Log($"Actual ammo: {currentClip}/{currentAmmo}");
        //Debug.Log($"Static variables: {RcurrentClip}/{RcurrentAmmo}");
    }
    public void AddAmmo(int ammoAmount)
    {
        currentAmmo += ammoAmount;
        if (currentAmmo > maxAmmoSize)
        {
            currentAmmo = maxAmmoSize;
        }
        Debug.Log($"WeaponScript, currentAmmo: {currentAmmo}");
    }
}
