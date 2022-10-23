using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviourPun
{
    public Image fillImage;

    public int localHealth = 100;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public BoxCollider2D collider;
    public GameObject playerCanvas;

    public PlayerHealth playerMovement;
    public void CheckHealth()
    {
        if(photonView.IsMine && localHealth <= 0)
        {
            //GameManager.Instance.EnableRespawn(); //respawn player in a new place
            //playerMovement.DisableInputs = true;
            this.GetComponent<PhotonView>().RPC("death", RpcTarget.AllBuffered);
        }
    }
    [PunRPC]
    public void death()
    {
        rb.gravityScale = 0;
        collider.enabled = false;
        sr.enabled = false;
        playerCanvas.SetActive(false);
    }
    [PunRPC]
    public void Revive()
    {
        rb.gravityScale = 1;
        collider.enabled = true;
        sr.enabled = true;
        playerCanvas.SetActive(true);
        localHealth = 100;
    }
    public void EnableInputs()
    {
        //playerMovement.DisableInputs = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
    public void HealthUpdate(int damage)
    {
        fillImage.fillAmount -= damage;
        localHealth -= damage;
    }
}
