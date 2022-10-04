using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Shooting : MonoBehaviour
{

    public Transform fire_Point;
    public GameObject bulletPrefab;
    private Animator animator;
    private bool shot = false;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            
            Debug.Log(shot);
        }
        else
            shot = false;
        animator.SetBool("shot", shot);
    }
    void Shoot ()
    {
        Instantiate(bulletPrefab, fire_Point.position, fire_Point.rotation);
        shot = true;
    }
}
