using System.Collections.Generic;
using Input;
using Interfaces;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour, IInputSystemListener
    {
        #region Variables

        [SerializeField] private PlayerMovement movement = new();
        [SerializeField] private PlayerShooter weapon = new();
        
        public PlayerMovement Movement => movement;
        public PlayerShooter Weapon => weapon;
        
        private Vector2 _movementInput = Vector2.zero;
        private Vector2 _aimingDirection = Vector2.right;
        private Vector3 _mousePos = Vector3.zero;
        private InputSystem _input;
        private bool _fireButtonDown;
        private readonly List<IUpdatable> _updatables = new();

        #endregion

        // TODO: and weapons, add health system, effects that modify player

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
            
            weapon.SetMuzzle(transform);
            
            _updatables.Add(weapon);
        }

        public void FireButtonPressed()
        {
            _fireButtonDown = true;
        }

        public void FireButtonReleased()
        {
            _fireButtonDown = false;
        }

        public void Interact()
        {
            // TODO: add interactions with objects
        }

        public void AbilityButtonPressed(int abilityId)
        {
            Debug.Log($"Pressed ability {abilityId}");
        }

        // Do main actions here
        private void Update()
        {
            _aimingDirection = _input.UsingMouse ? (_mousePos - transform.position).normalized : _input.AimingDirection;
            _mousePos = _input.CrosshairPosition;
            _movementInput = _input.MovementInput;
            
            if(_fireButtonDown)
                weapon.Shoot(_aimingDirection);

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
            Handles.Label(pos, "FacingRight: " + movement.IsFacingRight + "\nMovement info:\n" + movement.Speed);
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