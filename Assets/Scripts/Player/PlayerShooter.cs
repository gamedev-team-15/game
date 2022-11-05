using System;
using UnityEngine;
using UnityEngine.Search;

namespace Player
{
    [Serializable]
    public class PlayerShooter
    {
        [SerializeField] public Projectile.Projectile projectile;
        private Transform muzzle;
        
        public void Shoot(Vector2 v)
        {
            var prjct = GameObject.Instantiate(projectile, muzzle.position, muzzle.rotation);
            prjct.Initialize();
            prjct.Launch(v);
        }

        public void SetMuzzle(Transform t)
        {
            muzzle = t;
        }
    }
}