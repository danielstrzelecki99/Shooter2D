using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviourPun
{
    public Player Owner { get; private set; }
    public float speed;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    public float lifeTime; //time after bullet will be destroyed

    public float bulleteDamage = 0.15f;
    public void Start()
    {
        //invoke function destroying projectile after 'lifeTime'
        Invoke("DestroyProjectile", lifeTime);
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //check if PhotonView does not belong to player 
        if (!photonView.IsMine)
        {
            return;
        }
        PhotonView target = hitInfo.gameObject.GetComponent<PhotonView>();

        //destroy bullet only if it hits the other player
        if(target != null && (!target.IsMine || target.IsRoomView))
        {
            if (target.tag == "Player")
            {
                Debug.Log("Player was shot");
                target.RPC("HealthUpdate", RpcTarget.AllBuffered, bulleteDamage);
            }
            GetComponent<PhotonView>().RPC("DestroyProjectile", RpcTarget.AllBuffered);
        }
        //destroy bullet on ground/walls
        Debug.Log(hitInfo.name);
        GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(impact, 2);
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
    [PunRPC]
    public void DestroyProjectile()
    {
        GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(impact, 2);
        Destroy(gameObject);
    }
}
