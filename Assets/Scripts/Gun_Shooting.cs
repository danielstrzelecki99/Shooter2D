using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Shooting : MonoBehaviour
{

    public Transform fire_Point;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shooot();
        }
    }
    void Shooot ()
    {
        Instantiate(bulletPrefab, fire_Point.position, fire_Point.rotation);
    }
}
