using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsIgnore : MonoBehaviour
{
    void Start()
    {
        Physics2D.IgnoreLayerCollision(9, 8);
    }
   
}
