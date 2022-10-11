using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    // Start is called before the first frame update
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (rotZ < 89 && rotZ > -89)
        {
            Debug.Log("Facing right");
            spriteRender.flipY = false;
        }
        else
        {
            Debug.Log("Facing left");
            spriteRender.flipY = true;
        }
    }
}
