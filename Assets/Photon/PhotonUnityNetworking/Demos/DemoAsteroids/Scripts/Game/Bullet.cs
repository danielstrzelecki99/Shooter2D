using Photon.Realtime;
using UnityEngine;

namespace Photon.Pun.Demo.Asteroids
{
    public class Bullet : MonoBehaviour
    {
        public Player Owner { get; private set; }
        public float speed;
        public Rigidbody2D rb;
        public GameObject impactEffect;

        public void Start()
        {
            Destroy(gameObject, 3.0f);
            rb.velocity = transform.right * speed;
        }

        void OnTriggerEnter2D(Collider2D hitInfo)
        {
            Debug.Log(hitInfo.name);
            //Instantiate(impactEffect, transform.position, transform.rotation);
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
}