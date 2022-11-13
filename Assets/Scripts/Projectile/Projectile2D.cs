using UnityEngine;

namespace Projectile
{
    public abstract class Projectile2D : MonoBehaviour
    {
        protected const float LifeTime = 20f;
        [SerializeField] private ModVal.ModifiableValueInt damage = new(10);
        public ModVal.ModifiableValueInt Damage => damage;

        public void Initialize()
        {
            Initialize(Vector2.zero);
        }
        public abstract void Initialize(Vector2 initialVelocity);
        public abstract void Launch(Vector2 direction);
        public abstract void Launch(Transform target);
    }
}