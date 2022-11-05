using UnityEditor;
using UnityEngine;

namespace Projectile
{
    public abstract class Projectile : MonoBehaviour
    {
        public const float LifeTime = 20f;
        public abstract void Initialize();
        public abstract void Launch(Vector2 v);
        public abstract void Launch(Transform t);
    }
}