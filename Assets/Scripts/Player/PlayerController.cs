using System.Collections.Generic;
using GameAssets;
using Input;
using Interfaces;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour, IInputSystemListener, IEffect, IPlayerConfigLoader
    {
        #region Variables

        [SerializeField] private PlayerMovement movement = new();
        private readonly PlayerStats _stats = new();
        private readonly PlayerAbilities _abilities = new();
        private readonly PlayerWeapons _weapons = new();

        public PlayerMovement Movement => movement;
        public PlayerStats Stats => _stats;
        public PlayerAbilities Abilities => _abilities;
        public PlayerWeapons Weapons => _weapons;

        private Vector2 _movementInput = Vector2.zero;
        private Vector2 _aimingDirection = Vector2.right;
        private Vector3 _mousePos = Vector3.zero;
        private InputSystem _input;
        private bool _fireButtonDown;
        private readonly List<IUpdatable> _updatables = new();

        #endregion

        // TODO: and weapons, add health system
        // Abilities: in progress

        #region Methods

        private void Start()
        {
            // Init rigidbody
            if (!gameObject.TryGetComponent(out Rigidbody2D rb2d))
                rb2d = gameObject.AddComponent<Rigidbody2D>();
            rb2d.gravityScale = 0;
            rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;

            // Set up movement
            movement.SetRigidbody(rb2d);

            // Register input listener
            _input = FindObjectOfType<InputSystem>();
            if (_input is null)
            {
                Debug.LogError("InputSystem not found!");
                enabled = false;
                return;
            }

            _input.AddInputListener(this);

            _stats.SetPlayer(this);
            _abilities.SetPlayer(this);
            _weapons.SetPlayer(this);

            _updatables.Add(_weapons);
            _updatables.Add(_stats);
            _updatables.Add(_abilities);
        }

        public void FireButtonPressed()
        {
            _fireButtonDown = true;
        }

        public void FireButtonReleased()
        {
            _fireButtonDown = false;
        }

        public void ApplyEffect(StatusEffect effect)
        {
            _stats.ApplyEffect(effect);
        }

        public void Interact()
        {
            // TODO: add interactions with objects
        }

        public void AbilityButtonPressed(int abilityId)
        {
            _abilities.ActivateAbility(abilityId);
        }


        public void LoadConfig(PlayerConfig config)
        {
            movement.LoadConfig(config);
            _abilities.LoadConfig(config);
            _weapons.LoadConfig(config);
        }

        // Do main actions here
        private void Update()
        {
            _mousePos = _input.CrosshairPosition;
            _aimingDirection = _input.UsingMouse ? (_mousePos - transform.position).normalized : _input.AimingDirection;
            _movementInput = _input.MovementInput;

            _weapons.SetDirection(_aimingDirection);

            if (_fireButtonDown)
                _weapons.Shoot();

            foreach (var updatable in _updatables)
                updatable.Update(Time.deltaTime);
        }

        // Process physics-related stuff
        private void FixedUpdate()
        {
            movement.Move(_movementInput);
        }

        private void OnDestroy()
        {
            _input.RemoveInputListener(this);
        }

        #endregion

        #region Debug

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var pos = transform.position;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(pos, _movementInput);
            GUI.color = Gizmos.color;
            Handles.Label(pos,
                "FacingRight: " + movement.IsFacingRight +
                $"\nMovement info:\nBase: {Movement.Speed.BaseValue}\nCurrent: {Movement.Speed.Value}");
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(pos, _aimingDirection * 2);

            if (!_input || _input.UsingMouse) return;
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(pos, _input.AimingDirection * 2);
        }
#endif

        #endregion
    }
}