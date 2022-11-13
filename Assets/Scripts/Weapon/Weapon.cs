using System;
using System.Collections;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Weapon
{
    [Serializable]
    public class Weapon
    {
        [SerializeField] private Projectile.Projectile2D projectile;
        [SerializeField] [Min(0)] private float cooldown = 0.5f;
        [SerializeField] [Min(0)] private float burstCooldown;
        [SerializeField] [Min(1)] private int bulletsPerShot = 1;
        [SerializeField] [Range(0, 360)] private float spread;
        [SerializeField] [Min(0)] private float recoil;
        [SerializeField] private FireMode fireMode = FireMode.Auto;

        public float Cooldown => cooldown;
        public float Recoil => recoil;

        private Vector2 RotateVector(Vector2 v, float angle)
        {
            angle *= Mathf.Deg2Rad;
            var cos = Mathf.Cos(angle);
            var sin = Mathf.Sin(angle);
            return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
        }

        private IEnumerator Burst(Transform muzzle, Proxy<Vector2> velocity)
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                var prj = Object.Instantiate(projectile, muzzle.position, muzzle.rotation);
                prj.Initialize(velocity.Value);
                prj.Launch(RotateVector(muzzle.right, Random.Range(-spread / 2, spread / 2)));
                yield return new WaitForSeconds(burstCooldown);
            }

            yield return null;
        }

        private IEnumerator Shotgun(Transform muzzle, Proxy<Vector2> velocity)
        {
            var sDir = RotateVector(muzzle.right, -spread / 2);
            
            for (int i = 0; i < bulletsPerShot; i++)
            {
                var prj = Object.Instantiate(projectile, muzzle.position, muzzle.rotation);;
                prj.Initialize(velocity.Value);
                prj.Launch(sDir);
                sDir = RotateVector(sDir, spread / bulletsPerShot);
            }
            
            yield return null;
        }

        public IEnumerator Shoot(Transform muzzle, Proxy<Vector2> velocity)
        {
            switch (fireMode)
            {
                case FireMode.Burst or FireMode.Auto:
                    yield return Burst(muzzle, velocity);
                    break;
                case FireMode.Shotgun:
                    yield return Shotgun(muzzle, velocity);
                    break;
            }
        }
    }
}