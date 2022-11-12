using System.Collections;
using UnityEngine;

namespace Weapon
{
    public class Weapon : MonoBehaviour
    {
        
        [SerializeField] private Projectile.Projectile projectile;
        [SerializeField] [Min(0)] private float cooldown;
        [SerializeField] [Min(1)] private int burstCount;
        [SerializeField] [Min(0)] private float burstInterval;
        [SerializeField] [Min(1)] private int shotgunBulletCount;
        private float _timer;
        private bool shooting;
        public Transform _shotPoint;
        [SerializeField] FireMode fireMode = FireMode.Auto;
        private const int spread = 40;

        private Vector2 vectorAngle(Vector2 direction)
        {
            float x = direction.x;
            float y = direction.y;

            float angle = (spread / 4) * Mathf.Deg2Rad;
            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin (angle);

            float x2 = x * cos - y * sin;
            float y2 = x * sin + y * cos;
            Vector2 direction2 = new Vector2(x2,y2);
            
            return direction2;
        }
        private Vector2 vectorRotator(Vector2 direction)
        {
            float x = direction.x;
            float y = direction.y;

            float angle = (-spread / shotgunBulletCount) * Mathf.Deg2Rad;
            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin (angle);

            float x2 = x * cos - y * sin;
            float y2 = x * sin + y * cos;
            Vector2 direction2 = new Vector2(x2,y2);
            
            return direction2;
        }

        private IEnumerator burst(Vector2 direction)
        {
            shooting = true;
            for (int i = 0; i < burstCount; i++)
            {
                var project = Object.Instantiate(projectile, _shotPoint.position, _shotPoint.rotation);
                project.Initialize();
                project.Launch(direction);
                yield return new WaitForSeconds(burstInterval);
            }
            shooting = false;
            yield return null;
        }

        private void shotgun (Vector2 direction)
        {
            Vector2 directionTempo = direction;
            //vectorAngle(direction);
            for (int i = 0; i < shotgunBulletCount; i++)
            {
                var project = Object.Instantiate(projectile, _shotPoint.position, _shotPoint.rotation);
                project.Initialize();
                if (i == 0)
                {
                    vectorAngle(direction);
                    directionTempo = vectorAngle(direction);
                }
                else
                {
                    vectorRotator(directionTempo);
                    directionTempo = vectorRotator(directionTempo);
                }
                project.Launch(vectorAngle(directionTempo));

                
            }
        }
        
        
        
        public void Shoot(Vector2 direction)
        {
            if(_timer > 0 || shooting) return;
            switch (fireMode)
            {
                case FireMode.Burst:
                case FireMode.Auto:
                    StartCoroutine(burst(direction));
                    break;
                case FireMode.Shotgun:
                    shotgun(direction);
                    break;
            }
            _timer = cooldown;
        }

        public void SetShotPoint(Transform shotPoint)
        {
            _shotPoint = shotPoint;
        }
        
        public void Update()
        {
            if (!shooting)
            {
                _timer -= Time.deltaTime;
            }
        }

    }
}
