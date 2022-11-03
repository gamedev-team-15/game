using System;
using UnityEngine;

namespace Editor
{
    [RequireComponent(typeof(Collider2D))]
    public class KnockbackTest : MonoBehaviour
    {
        [SerializeField] [Range(0, 10)] private float force = 1;
        // TODO: improve knockback on player and other rigidbodies
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player.PlayerController p))
            {
                p.Movement.Rb2D.AddForce((p.transform.position - transform.position).normalized * force * 10, ForceMode2D.Impulse);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player.PlayerController p))
            {
                p.Movement.Rb2D.AddForce((p.transform.position - transform.position).normalized * force * 10, ForceMode2D.Impulse);
            }
        }
    }
}