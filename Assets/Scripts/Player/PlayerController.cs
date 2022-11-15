using System.Collections.Generic;
using Core;
using GameAssets;
using Input;
using Interfaces;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class PlayerController : GameEntity, IInputSystemListener, IEffect, IPlayerConfigLoader, IDamage, IHeal
    {
        #region Variables

        // All player sub-components
        public PlayerMovement Movement { get; } = new();
        public PlayerStats Stats { get; } = new();
        public PlayerAbilities Abilities { get; } = new();
        public PlayerWeapons Weapons { get; } = new();
        public PlayerHealth Health { get; } = new();
        public PlayerEvents Events { get; } = new();

        private Vector2 _movementInput = Vector2.zero;
        private Vector2 _aimingDirection = Vector2.right;
        private Vector3 _mousePos = Vector3.zero;
        private InputSystem _input;
        private bool _fireButtonDown;
        private readonly List<IUpdatable> _updatables = new();

        #endregion

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
            Movement.SetRigidbody(rb2d);

            // Register input listener
            _input = FindObjectOfType<InputSystem>();
            if (_input is null)
            {
                Debug.LogError("InputSystem not found!");
                enabled = false;
                return;
            }

            _input.AddInputListener(this);

            Stats.SetPlayer(this);
            Abilities.SetPlayer(this);
            Weapons.SetPlayer(this);

            _updatables.Add(Weapons);
            _updatables.Add(Stats);
            _updatables.Add(Abilities);
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
            Stats.ApplyEffect(effect);
        }

        public void Interact()
        {
            Events.OnPlayerInteract.Invoke();
        }

        public void AbilityButtonPressed(int abilityId)
        {
            Abilities.ActivateAbility(abilityId);
        }
        
        public void ApplyDamage(int damage)
        {
            Health.ApplyDamage(damage);
            if(Health.CurrentHealth <= 0)
                Events.OnPlayerDeath.Invoke();
        }

        public void ApplyHeal(int hp)
        {
            Health.ApplyHeal(hp);
        }
        
        public void LoadConfig(PlayerConfig config)
        {
            Movement.LoadConfig(config);
            Abilities.LoadConfig(config);
            Weapons.LoadConfig(config);
            Health.LoadConfig(config);
        }

        // Do main actions here
        private void Update()
        {
            _mousePos = _input.CrosshairPosition;
            _aimingDirection = _input.UsingMouse ? (_mousePos - transform.position).normalized : _input.AimingDirection;
            _movementInput = _input.MovementInput;

            Weapons.SetDirection(_aimingDirection);

            if (_fireButtonDown)
                Weapons.Shoot();

            foreach (var updatable in _updatables)
                updatable.Update(Time.deltaTime);
        }

        // Process physics-related stuff
        private void FixedUpdate()
        {
            Movement.Move(_movementInput);
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
                "FacingRight: " + Movement.IsFacingRight +
                $"\nHealth: {Health.CurrentHealth}/{Health.MaxHealth}" +
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