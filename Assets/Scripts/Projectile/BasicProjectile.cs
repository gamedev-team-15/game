using System;
using UnityEngine;

namespace Projectile
{
    public class BasicProjectile : Projectile
    {
        [SerializeField]
        private float force = 10f;
        [SerializeField] 
        private Rigidbody2D rb2d;

        public override void Initialize()
        {
            Destroy(gameObject, LifeTime);
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log(col);
            Destroy(gameObject);
        }

        public override void Launch(Vector2 v)
        {
            transform.right = v;
            rb2d.AddForce(transform.right * force, ForceMode2D.Impulse);
        }

        public override void Launch(Transform t)
        {
            Launch((t.position - transform.position).normalized);
        }
    }
}