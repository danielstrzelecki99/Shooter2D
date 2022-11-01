using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerEq : MonoBehaviourPun
{
    public static int armorAmount = 0;
    public static int ammoAmount = 0;
    public static int aidKitAmount = 0;
    public static bool destroy = false;
    
    void Update()
    {
        if(PlayerMovement.pickUpAllowed == true && Input.GetKeyDown(KeyCode.E)){
            destroy = true;
        }
        if(aidKitAmount > 0 && Input.GetKeyDown(KeyCode.F) && PlayerHealth.localHealth != 1){
            aidKitAmount -= 1;
            GetComponent<PhotonView>().RPC("Heal", RpcTarget.AllBuffered);
        }
        if(armorAmount > 0 && Input.GetKeyDown(KeyCode.G) && PlayerHealth.localArmor != 1){
            armorAmount -= 1;
            GetComponent<PhotonView>().RPC("ArmorUse", RpcTarget.AllBuffered);
        }
    }
}
