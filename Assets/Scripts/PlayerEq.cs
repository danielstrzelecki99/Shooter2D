using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerEq : MonoBehaviour
{
    public static int armorAmount = 0;
    public static int ammoAmount = 0;
    public static int aidKitAmount = 0;
    public static bool destroy = false;
  
    // Start is called before the first frame update
    void Update()
    {
        if(ItemsManager.pickUpAllowed == true && Input.GetKeyDown(KeyCode.E)){
            destroy = true;
        }
    }
}
