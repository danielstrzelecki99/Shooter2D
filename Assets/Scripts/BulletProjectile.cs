using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public Player Owner { get; private set; }
    public float speed;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    public float radius = 3;
    public int damageAmount = 15;

    GunInfo gunInfo;

    private void Awake()
    {
        gunInfo = GetComponent<GunInfo>();
    }
    public void Start()
    {
        Destroy(gameObject, 3.0f);
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(impact, 2);
        hitInfo.gameObject.GetComponent<IDamagable>()?.TakeDamage(gunInfo.damage);
        Destroy(gameObject);
    }

    public void InitializeBullet(Player owner, Vector3 originalDirection, float lag)
    {
        Owner = owner;

        transform.forward = originalDirection;

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = originalDirection * 200.0f;
        rigidbody.position += rigidbody.velocity * lag;
    }
}
