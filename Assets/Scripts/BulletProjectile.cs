using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviourPun
{
    public float speed;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    public float lifeTime = 3f; //time after bullet will be destroyed
    public static float bulleteDamage = 0.15f;

    public void Start()
    {
        //invoke function destroying projectile after 'lifeTime'
        //Invoke("DestroyProjectile", lifeTime);
        rb.velocity = transform.right * speed;
    }

    IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(lifeTime);
        GetComponent<PhotonView>().RPC("DestroyProjectile", RpcTarget.AllBuffered);
    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //check if PhotonView does not belong to player 
    //    if (!photonView.IsMine)
    //    {
    //        return;
    //    }
    //    //create variable for enemy
    //    PhotonView target = collision.gameObject.GetComponent<PhotonView>();
    //    Debug.Log($"Target: {target}");
    //    //condition if bullet hits someone
    //    if(target != null && (!target.IsMine || target.IsRoomView))
    //    {
    //        Debug.Log("First condition passed");
    //        if (target.CompareTag("Player"))
    //        {
    //            Debug.Log("Player has been shot");
    //            //update health hitten player
    //            target.RPC("HealthUpdate", RpcTarget.AllBuffered, bulleteDamage);
    //        }
    //        GetComponent<PhotonView>().RPC("DestroyProjectile", RpcTarget.AllBuffered);
    //    }
    //    //destroy bullet on ground/walls
    //    //Debug.Log("Ground/wall has been shot");
    //    GetComponent<PhotonView>().RPC("DestroyProjectile", RpcTarget.AllBuffered);
    //}
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!photonView.IsMine)
        {
            return;
        }
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null && (!target.IsMine || target.IsRoomView))
        {
            if (target.CompareTag("Player"))
            {
                //Debug.Log("Player has been shot");
                //update health hitten player
                target.RPC("HealthUpdate", RpcTarget.AllBuffered, bulleteDamage);
            }
            GetComponent<PhotonView>().RPC("DestroyProjectile", RpcTarget.AllBuffered);
        }
        //destroy bullet on ground/walls
        //Debug.Log("Ground/wall has been shot");
        GetComponent<PhotonView>().RPC("DestroyProjectile", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
