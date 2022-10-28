using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public static GameObject selectedObject;
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
            PlayerEq.aidKitAmount += 1;
        } else if(selectedObject.tag == "AmmoKit"){
            PlayerEq.ammoAmount += 1;
        } else if (selectedObject.tag == "Armor"){
            PlayerEq.armorAmount += 1;
        }
        Destroy(selectedObject);
    }
}
