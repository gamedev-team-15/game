using UnityEngine;

namespace Projectile
{
    public abstract class Projectile : MonoBehaviour
    {
        protected const float LifeTime = 20f;
        [SerializeField] private ModVal.ModifiableValueInt damage = new(10);
        public ModVal.ModifiableValueInt Damage => damage;

        public abstract void Initialize();
        public abstract void Launch(Vector2 direction);
        public abstract void Launch(Transform target);
    }
}