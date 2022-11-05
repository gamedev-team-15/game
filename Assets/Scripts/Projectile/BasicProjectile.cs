using UnityEngine;

namespace Projectile
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BasicProjectile : Projectile
    {
        [SerializeField] private float force = 10f;
        private Rigidbody2D _rb2d;

        public override void Initialize()
        {
            _rb2d = transform.GetComponent<Rigidbody2D>();
            Destroy(gameObject, LifeTime);
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log(col);
            Destroy(gameObject);
        }

        public override void Launch(Vector2 direction)
        {
            transform.right = direction;
            _rb2d.AddForce(direction * force, ForceMode2D.Impulse);
        }

        public override void Launch(Transform target)
        {
            Launch((target.position - transform.position).normalized);
        }
    }
}