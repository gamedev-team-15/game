using GameAssets;
using Interfaces;
using UnityEngine;

namespace Projectile
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BasicProjectile : Projectile
    {
        [SerializeField] private float force = 10f;
        [SerializeField] private float knockback;
        [SerializeField] private StatusEffect effect;
        private Rigidbody2D _rb2d;

        public override void Initialize()
        {
            Damage.ClearModifiers();
            _rb2d = transform.GetComponent<Rigidbody2D>();
            Destroy(gameObject, LifeTime);
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            if(col.usedByEffector) return;
            
            foreach (var behaviour in col.gameObject.GetComponents<MonoBehaviour>())
                if(effect && behaviour is IEffect m)
                    m.ApplyEffect(effect);

            if (col.gameObject.TryGetComponent(out Rigidbody2D rb))
                rb.AddForce((col.gameObject.transform.position - transform.position).normalized * knockback, ForceMode2D.Impulse);

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