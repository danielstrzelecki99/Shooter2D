using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviourPun
{
    public Image fillImage;
    public Image armorFillImage;
    public static float localHealth = 1;
    public static float localArmor = 0;

    //variables required to be hidden when player is dead
    public Rigidbody2D rb;
    //public SpriteRenderer sr;
    public GameObject playerPrefab; //against SpriteRenderer
    public BoxCollider2D playerCollider;
    public GameObject playerCanvas;
    //reference to PlayerMovement script
    PlayerMovement playerScript;
    Gun_Shooting shootingScript;
    WeaponManager weaponManager;

    public void Start()
    {
        armorFillImage.fillAmount = localArmor;
    }
    //check health level 
    public void CheckHealth()
    {
        //condition only for specific (local) 
        if(photonView.IsMine && localHealth <= 0)
        {
            GameManagerScript.instance.EnableRespawn(); //respawn player in a new place
            playerScript.DisableInputs = true; //disable inputs like jump and move
            shootingScript.DisableInputs = true; //disable shooting and moving weapon
            weaponManager.DisableInputs = true; //disable switching guns
            GetComponent<PhotonView>().RPC("Death", RpcTarget.AllBuffered);
        }
    }
    [PunRPC]
    public void Death()
    {
        rb.gravityScale = 0;
        playerCollider.enabled = false;
        //sr.enabled = false;
        playerCanvas.SetActive(false);
        Destroy(gameObject);
    }
    [PunRPC]
    public void Revive()
    {
        rb.gravityScale = 1;
        playerCollider.enabled = true;
        //sr.enabled = true;
        playerCanvas.SetActive(true);
        localHealth = 1;
        fillImage.fillAmount = localHealth;
        Debug.Log("Player has respawned again");
    }
    public void EnableInputs()
    {
        playerScript.DisableInputs = false;
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
    public void ArmorUse(){
        localArmor = 1;
        armorFillImage.fillAmount = localArmor;
    }
}
