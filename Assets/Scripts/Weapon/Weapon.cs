using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Weapon
{
    [Serializable]
    public class Weapon
    {
        [SerializeField] private Projectile.Projectile projectile;
        [SerializeField] [Min(0)] private float cooldown = 0.5f;
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
            var sin = Mathf.Sin (angle);
            return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
        }
        
        private IEnumerator Burst(Vector2 position, Vector2 direction)
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                var project = Object.Instantiate(projectile);
                projectile.transform.position = position;
                project.Initialize();
                project.Launch(direction);
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }

        private IEnumerator Shotgun(Vector2 position, Vector2 direction)
        {
            var sDir = RotateVector(direction, -spread / 2);
            for (int i = 0; i < bulletsPerShot; i++)
            {
                var prj = Object.Instantiate(projectile);
                prj.transform.position = position;
                prj.Initialize();
                Debug.Log(sDir);
                prj.Launch(sDir);
                sDir = RotateVector(sDir, spread / bulletsPerShot);
            }
            yield return null;
        }
        
        
        
        public IEnumerator Shoot(Vector2 position, Vector2 direction)
        {
            switch (fireMode)
            {
                case FireMode.Burst or FireMode.Auto:
                {
                    yield return Burst(position, direction);
                    break;
                }
                case FireMode.Shotgun:
                    yield return Shotgun(position, direction);
                    break;
            }
        }
    }
}
