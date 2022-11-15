using Core;
using GameAssets;
using Interfaces;
using UnityEngine;

namespace Projectile
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BasicProjectile2D : Projectile2D
    {
        [SerializeField] private float force = 10f;
        [SerializeField] private float knockback;
        [SerializeField] private StatusEffect effect;
        [SerializeField] private int piercingPower = 1;
        private int _piercingPowerLeft = 1;
        private Rigidbody2D _rb2d;

        public override void Initialize(Vector2 initialVelocity)
        {
            Damage.ClearModifiers();
            _rb2d = transform.GetComponent<Rigidbody2D>();
            _rb2d.velocity = initialVelocity;
            _piercingPowerLeft = piercingPower;
            Destroy(gameObject, LifeTime);
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            if (col.usedByEffector) return;
            bool staticObjectHit = true;

            foreach (var behaviour in col.gameObject.GetComponents<MonoBehaviour>())
            {
                if (behaviour is IDamage d)
                {
                    d.ApplyDamage(Damage.Value);
                    _piercingPowerLeft--;
                    RuntimeManager.Utils.CreateTextPopup(transform.position, Damage.Value.ToString(), 8);
                }

                if (effect && behaviour is IEffect m)
                    m.ApplyEffect(effect);
                staticObjectHit = false;
            }

            if (col.gameObject.TryGetComponent(out Rigidbody2D rb))
                rb.AddForce((col.gameObject.transform.position - transform.position).normalized * knockback,
                    ForceMode2D.Impulse);

            if(staticObjectHit || _piercingPowerLeft <= 0)
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