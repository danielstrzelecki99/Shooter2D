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
    public static string itemUseInfo;
    public static bool destroy = false;
    public static bool useAidKit = false;
    
  
    // Start is called before the first frame update
    void Update()
    {
        if(PlayerMovement.pickUpAllowed == true && Input.GetKeyDown(KeyCode.E)){
            destroy = true;
        }

        if(aidKitAmount > 0 && Input.GetKeyDown(KeyCode.F) && PlayerHealth.localHealth != 1){
            aidKitAmount -= 1;
            GetComponent<PhotonView>().RPC("Heal", RpcTarget.AllBuffered);
        } else {
            itemUseInfo = "You don't have any first aid kit!";
        }
    }
}
