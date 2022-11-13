using System;
using System.Collections;
using GameAssets;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerWeapons : IUpdatable, IPlayerConfigLoader
    {
        private PlayerController _player;
        private Transform _weaponTransform;
        private Transform _weaponMuzzle;
        private SpriteRenderer _weaponRenderer;
        private WeaponContainer _activeWeapon;

        private void SetUp()
        {
            if (_weaponTransform) return;
            _weaponTransform = new GameObject("PlayerGun").transform;
            var weaponRendererTransform = new GameObject("WeaponSprite", typeof(SpriteRenderer)).transform;
            _weaponRenderer = weaponRendererTransform.GetComponent<SpriteRenderer>();
            _weaponRenderer.sortingOrder = 1;
            weaponRendererTransform.parent = _weaponTransform;
            weaponRendererTransform.position = new Vector3(0.4f, -0.1f, 0);
            _weaponMuzzle = new GameObject("GunMuzzle").transform;
            _weaponMuzzle.parent = weaponRendererTransform;
            _weaponMuzzle.position = Vector3.zero;
        }
        
        public void Shoot()
        {
            _activeWeapon.Shoot(_player,_weaponMuzzle.position, _weaponMuzzle.right);
        }
        
        public void Update(float deltaTime)
        {
            _activeWeapon.Update(deltaTime);
        }

        public void LoadConfig(PlayerConfig config)
        {
            _activeWeapon = new WeaponContainer(config.DefaultWeapon);
            SetUp();
            _weaponRenderer.sprite = config.DefaultWeapon.Sprite;
            _weaponMuzzle.position = config.DefaultWeapon.MuzzleOffset;
        }

        public void SetPlayer(PlayerController player)
        {
            _player = player;
            SetUp();
            _weaponTransform.parent = player.transform;
        }

        public void SetDirection(Vector2 aimingDirection)
        {
            _weaponTransform.right = aimingDirection;
            _weaponRenderer.flipY = aimingDirection.x < 0;
        }
        
        private class WeaponContainer : IUpdatable
        {
            public WeaponData Data { get; }
            private float _timer;
            private bool _isReady;
            
            public WeaponContainer(WeaponData data)
            {
                Data = data;
                _isReady = true;
            }
            
            public void Update(float deltaTime)
            {
                if(_isReady)
                    _timer -= deltaTime;
            }

            public void Shoot(MonoBehaviour mono, Vector2 position, Vector2 direction)
            {
                if (_timer > 0 || !_isReady) return;
                mono.StartCoroutine(StartShooting(position, direction));
                _timer = Data.Weapon.Cooldown;
            }

            private IEnumerator StartShooting(Vector2 position, Vector2 direction)
            {
                _isReady = false;
                yield return Data.Weapon.Shoot(position, direction);
                _isReady = true;
            }
        }
    }
}