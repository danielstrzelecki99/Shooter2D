using Photon.Realtime;
using UnityEngine;

namespace Photon.Pun.Demo.Asteroids
{
    public class Bullet : MonoBehaviour
    {
        public Player Owner { get; private set; }

        public void Start()
        {

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