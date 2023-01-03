using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviourPun
{
    public string gameMode;

    public Image fillImage;
    public Image armorFillImage;
    public float localHealth = 1;
    public float localArmor = 0;
    public static float slocalArmor;
    public static float slocalHealth;

    //variables required to be hidden when player is dead
    public Rigidbody2D rb;
    public GameObject playerCanvas;

    //reference to Player script
    public PlayerMovement playerScript;
    public Gun_Shooting shootingScript;
    public WeaponManager weaponManager;
    PhotonView view;

    //variables to reset ammo in riffle when respawn 
    [SerializeField] private GameObject riffle;
    WeaponScript riffleController;

    public void Start()
    {
        armorFillImage.fillAmount = localArmor;
        view = GetComponent<PhotonView>();
        riffleController = riffle.GetComponent<WeaponScript>();
        gameMode = PhotonNetwork.CurrentRoom.CustomProperties["GameMode"].ToString();
    }
    private void Update()
    {
        if(view.IsMine){
            slocalArmor = localArmor;
            slocalHealth = localHealth;
        }
    }
    //check health level 
    public void CheckHealth()
    {
        //condition only for specific (local) 
        if(photonView.IsMine && localHealth <= 0)
        {
            localHealth = 0;
            playerScript.DisableInputs = true; //disable inputs like jump and move
            shootingScript.DisableInputs = true; //disable shooting and moving weapon
            weaponManager.DisableInputs = true; //disable switching guns
            GetComponent<PhotonView>().RPC("Death", RpcTarget.AllBuffered);
            PlayerEq.deathsInGame += 1;
            if (gameMode == "Deathmatch")
            {
                GameManagerScript.instance.EnableRespawn(); //respawn player in a new place
            }
            else
            {
                Timer.instance.OnEnd();
            }
        }
    }

    [PunRPC]
    public void Death()
    {
        rb.gravityScale = 0;
        playerCanvas.SetActive(false);
        gameObject.SetActive(false);
        Debug.Log("Player model has been destroyed");
    }
    [PunRPC]
    public void Revive()
    {
        rb.gravityScale = 1;
        playerCanvas.SetActive(true);
        gameObject.SetActive(true);
        //set health level and image to 1
        localHealth = 1;
        fillImage.fillAmount = localHealth;
        //set armor level and image to 1
        localArmor = 0;
        armorFillImage.fillAmount = localArmor;
        Debug.Log("Player has respawned again");
        riffleController.currentClip = 20;
        riffleController.currentAmmo = 40;
    }
    public void EnableInputs()
    {
        Debug.Log($"Enable inputs method");
        playerScript.DisableInputs = false;
        shootingScript.DisableInputs = false;
        weaponManager.DisableInputs = false;
    }
    [PunRPC]
    public void HealthUpdate(float damage)
    {
        fillImage.fillAmount -= damage;
        localHealth = fillImage.fillAmount;
        localHealth -= damage;
        CheckHealth();
    }
    [PunRPC]
    public void Heal(){
        localHealth = 1;
        fillImage.fillAmount = localHealth;
    }
    [PunRPC]
    public void ArmorUpdate(float damage)
    {
        armorFillImage.fillAmount -= damage;
        localArmor = armorFillImage.fillAmount;
        localArmor -= damage;
        if(localArmor <= 0)
        {
            localArmor = 0;
            localArmor = armorFillImage.fillAmount;
        }
    }
    [PunRPC]
    public void ArmorUse(){
        localArmor = 1;
        armorFillImage.fillAmount = localArmor;
    }

    //Checking methods for items system
        public bool isHealthFull()
    {
        if(localHealth == 1){
            return true;
        } else {
            return false;
        }
    }

    public bool isArmorFull()
    {
        if(localArmor == 1){
            return true;
        } else {
            return false;
        }
    }

    public bool isArmorUsed()
    {
        if(localArmor > 0){
            return true;
        } else {
            return false;
        }
    }
}
