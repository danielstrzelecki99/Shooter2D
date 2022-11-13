using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerEq : MonoBehaviourPunCallbacks
{
    public static int armorAmount = 0;
    public static int ammoAmount = 0;
    public static int aidKitAmount = 0;
    public static int killsInGame = 0;
    public static int deathsInGame = 0;
    public static int damageDealtInGame = 0;
    public static float pointsInGame = 0;
    public static bool destroy = false;
    public static bool useAidKit = false;
    
    PhotonView view;
    PlayerHealth playerHealth;
    PlayerHealth playerArmor;
    public GameObject player;

    private void Awake() {
        playerHealth = player.GetComponent<PlayerHealth>();
        playerArmor = player.GetComponent<PlayerHealth>();
    }
    public void Start()
    {
         view = GetComponent<PhotonView>();
    }
    public override void OnEnable()
    {
        armorAmount = 0;
        ammoAmount = 0;
        aidKitAmount = 0;
    }

    void Update()
    {
        if(view.IsMine){
            if(PlayerMovement.pickUpAllowed == true && Input.GetKeyDown(KeyCode.E)){
                destroy = true;
            }
            if(aidKitAmount > 0 && Input.GetKeyDown(KeyCode.F) && !playerHealth.isHealthFull()){
                    aidKitAmount -= 1;
                    GetComponent<PhotonView>().RPC("Heal", RpcTarget.AllBuffered);
            }
            if(armorAmount > 0 && Input.GetKeyDown(KeyCode.G) && !playerArmor.isArmorFull()){
                armorAmount -= 1;
                GetComponent<PhotonView>().RPC("ArmorUse", RpcTarget.AllBuffered);
            }
            UpdatePoints();
        }
    }

    public void UpdatePoints()
    {
        pointsInGame = (killsInGame * 100 + damageDealtInGame/10);
    }
}
