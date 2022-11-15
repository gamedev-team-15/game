using Core;
using GameAssets;
using Interfaces;
using UnityEngine;

namespace Projectile
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BasicProjectile2D : Projectile2D
    {
        [SerializeField] [Min(0)] private float force = 10f;
        [SerializeField] [Min(0)] private float knockback;
        [SerializeField] private StatusEffect effect;
        [SerializeField] [Min(1)] private int piercingPower = 1;
        [SerializeField] private ParticleSystem hitParticles;
        [SerializeField] private AudioClip hitSound;
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
            
            if (hitParticles)
                Instantiate(hitParticles, transform.position, transform.rotation);
            
            if(hitSound)
                AudioSource.PlayClipAtPoint(hitSound, transform.position, GameSettings.FxVolume);
            
            if (col.TryGetComponent(out IDamage d) && !(staticObjectHit = false))
                HandleHit(d);
            
            if(effect && col.TryGetComponent(out IEffect e) && !(staticObjectHit = false))
                e.ApplyEffect(effect);
            
            if (col.TryGetComponent(out Rigidbody2D rb) && !(staticObjectHit = false))
                rb.AddForce((col.gameObject.transform.position - transform.position).normalized * knockback,
                    ForceMode2D.Impulse);

            if(staticObjectHit || _piercingPowerLeft <= 0)
                Destroy();
        }

        protected virtual void HandleHit(IDamage target)
        {
            target.ApplyDamage(Damage.Value);
            _piercingPowerLeft--;
            RuntimeManager.Utils.CreateTextPopup(transform.position, Damage.Value.ToString(), 8);
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