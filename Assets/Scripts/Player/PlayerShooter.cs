using System;
using Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Player
{
    [Serializable]
    public class PlayerShooter : IUpdatable
    {
        [SerializeField] private Projectile.Projectile2D projectile;
        [SerializeField] [Min(0)] private float cooldown = 0.4f;
        private float _timer;
        private Transform _muzzle;

        public void Shoot(Vector2 direction)
        {
            if (_timer > 0) return;
            var project = Object.Instantiate(projectile, _muzzle.position, _muzzle.rotation);
            project.Initialize();
            project.Launch(direction);
            _timer = cooldown;
        }

        public void SetMuzzle(Transform muzzle)
        {
            _muzzle = muzzle;
        }

        public void Update(float deltaTime)
        {
            _timer -= deltaTime;
        }
    }
}