using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemsManager : MonoBehaviourPun
{
    public static GameObject selectedObject;
    public static string itemInfoText;
    public static float interval;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(9, 8);
    }

    void Update()
    {
        //selectedObject = gameObject;
        if(PlayerEq.destroy == true){
            PickUp();
            PlayerEq.destroy = false;
        }
    }

    public void PickUp()
    {   
        if(selectedObject.tag == "AidKit"){
            if(PlayerEq.aidKitAmount == 2){
                itemInfoText = "You already have 2 first aid kits!";
                interval = 2f;
                return;
            }
            else{
                PlayerEq.aidKitAmount += 1;
            }
        } else if(selectedObject.tag == "AmmoKit"){
            if(PlayerEq.ammoAmount == 2){
                itemInfoText = "You already have 2 ammo packs!";
                interval = 2f;
                return;
            }
            else{
                PlayerEq.ammoAmount += 1;
            }
        } else if (selectedObject.tag == "Armor"){
            if(PlayerEq.armorAmount == 2){
                itemInfoText = "You already have 2 armors!";
                interval = 2f;
                return;
            }
            else{
                PlayerEq.armorAmount += 1;
            }
        }
        Destroy(selectedObject);
    }
}
