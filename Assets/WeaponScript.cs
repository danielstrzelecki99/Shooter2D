using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        if (rotZ < 89 && rotZ > -89)
        {
            Debug.Log("Facing right");
            transform.Rotate(0f, 0f, transform.rotation.z);
        }
        else
        {
            Debug.Log("Facing left");
            transform.Rotate(180f, 0f, transform.rotation.z);
        }
    }
}
