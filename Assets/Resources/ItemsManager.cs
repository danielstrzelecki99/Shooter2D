using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
        //Interact with items on map variables
    public static string interactInfoText;
    public static bool pickUpAllowed = false;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(9, 8);
    }

    void Update()
    {
        if(PlayerEq.destroy == true){
            PickUp();
            PlayerEq.destroy = false;
        }
    }
   
    public void PickUp()
    {   
        if(gameObject.tag == "AidKit"){
            PlayerEq.aidKitAmount += 1;
        } else if(gameObject.tag == "AmmoKit"){
            PlayerEq.ammoAmount += 1;
        } else if (gameObject.tag == "Armor"){
            PlayerEq.armorAmount += 1;
        }
        Destroy(gameObject);
    }
    
    //Interact with items on map methods
    private void OnTriggerStay2D(Collider2D other) {
         if(other.gameObject.tag == "Player"){
            interactInfoText = "Press [E] to take item!";
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            interactInfoText = "";
            pickUpAllowed = false;
        }
    }
}
