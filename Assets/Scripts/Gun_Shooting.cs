using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Shooting : MonoBehaviour
{

    public Transform fire_Point;
    //public GameObject bulletPrefab;
    public GameObject impactEffect;
    private Animator animator;
    private bool shot = false;
    public LineRenderer lineRenderer;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Shoot());
        }
        else
            shot = false;
        animator.SetBool("shot", shot);
    }
    IEnumerator Shoot ()
    {
        //Instantiate(bulletPrefab, fire_Point.position, fire_Point.rotation);
        RaycastHit2D hitInfo = Physics2D.Raycast(fire_Point.position, fire_Point.right);
        shot = true;
        if (hitInfo)
        {
            Debug.Log(hitInfo.transform.name);
            Instantiate(impactEffect, hitInfo.point, Quaternion.identity);
            lineRenderer.SetPosition(0, fire_Point.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(0, fire_Point.position);
            lineRenderer.SetPosition(1, fire_Point.position + fire_Point.right * 100);
        }

        lineRenderer.enabled = true;
        // waiting to disable line
        yield return new WaitForSeconds(0.02f);
        lineRenderer.enabled = false;
    }
}
