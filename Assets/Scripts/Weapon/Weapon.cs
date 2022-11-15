using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using ModVal;
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

        private IEnumerator Burst(Transform muzzle, IValueProvider<Vector2> velocity, ICollection<ValueModifier<int>> damageModifiers)
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                var prj = Object.Instantiate(projectile, muzzle.position, muzzle.rotation);
                prj.Initialize(velocity.GetValue());
                prj.AddDamageModifiers(damageModifiers);
                prj.Launch(RotateVector(muzzle.right, Random.Range(-spread / 2, spread / 2)));
                yield return new WaitForSeconds(burstCooldown);
            }
        }

        private IEnumerator Shotgun(Transform muzzle, IValueProvider<Vector2> velocity, ICollection<ValueModifier<int>> damageModifiers)
        {
            var sDir = RotateVector(muzzle.right, -spread / 2);
            
            for (int i = 0; i < bulletsPerShot; i++)
            {
                var prj = Object.Instantiate(projectile, muzzle.position, muzzle.rotation);
                prj.Initialize(velocity.GetValue());
                prj.Damage.AddModifiers(damageModifiers);
                if (fireMode == FireMode.Shotgun)
                {
                    prj.Launch(sDir);
                    sDir = RotateVector(sDir, spread / bulletsPerShot);
                }
                else
                {
                    prj.Launch(RotateVector(muzzle.right, Random.Range(-spread / 2, spread / 2)));
                }
            }
            
            yield return null;
        }
        
        public IEnumerator Shoot(Transform muzzle, ICollection<ValueModifier<int>> damageModifiers)
        {
            return Shoot(muzzle, new ConstantValueProvider<Vector2>(Vector2.zero), damageModifiers);
        }

        public IEnumerator Shoot(Transform muzzle, IValueProvider<Vector2> velocity, ICollection<ValueModifier<int>> damageModifiers)
        {
            switch (fireMode)
            {
                case FireMode.Burst or FireMode.Auto:
                    yield return Burst(muzzle, velocity, damageModifiers);
                    break;
                case FireMode.Shotgun or FireMode.RandomizedShotgun:
                    yield return Shotgun(muzzle, velocity, damageModifiers);
                    break;
            }
        }
    }
}