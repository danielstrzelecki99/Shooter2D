using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
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
        Destroy(this.gameObject);
    }
}
