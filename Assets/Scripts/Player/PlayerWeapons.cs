using System.Collections;
using GameAssets;
using Interfaces;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerWeapons : IUpdatable, IPlayerConfigLoader
    {
        private PlayerController _player;
        private Transform _weaponTransform;
        private Transform _weaponMuzzle;
        private SpriteRenderer _weaponRenderer;
        private WeaponContainer _activeWeapon;
        private ProxyVelocity _proxyVelocity;

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
            _weaponMuzzle.localPosition = Vector3.zero;
        }

        public void Shoot()
        {
            _activeWeapon.Shoot(_player, _weaponMuzzle, _proxyVelocity);
        }

        public void Update(float deltaTime)
        {
            _activeWeapon.Update(deltaTime);
        }

        public void LoadConfig(PlayerConfig config)
        {
            PickUpWeapon(config.DefaultWeapon);
        }

        public void PickUpWeapon(WeaponData weapon)
        {
            SetUp();
            _activeWeapon = new WeaponContainer(weapon);
            _weaponRenderer.sprite = weapon.Sprite;
            _weaponMuzzle.localPosition = weapon.MuzzleOffset;
        }

        public void SetPlayer(PlayerController player)
        {
            _player = player;
            SetUp();
            _weaponTransform.parent = player.transform;
            _proxyVelocity = new ProxyVelocity(_player.Movement.Rb2D);
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
                if (_isReady)
                    _timer -= deltaTime;
            }

            public void Shoot(MonoBehaviour mono, Transform muzzle, ProxyVelocity velocity)
            {
                if (_timer > 0 || !_isReady) return;
                mono.StartCoroutine(StartShooting(muzzle, velocity));
                _timer = Data.Weapon.Cooldown;
            }

            private IEnumerator StartShooting(Transform muzzle, ProxyVelocity velocity)
            {
                _isReady = false;
                yield return Data.Weapon.Shoot(muzzle, new Proxy<Vector2>(velocity));
                _isReady = true;
            }
        }
    }
}