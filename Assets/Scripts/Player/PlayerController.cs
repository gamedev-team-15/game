using System;
using Input;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour, IInputSystemListener
    {
        #region Variables

        [SerializeField] private PlayerMovement movement = new();
        public PlayerMovement Movement => movement;
        private Vector2 _movementInput = Vector2.zero;
        private Vector2 _aimingDirection = Vector2.right;
        private Vector3 _mousePos = Vector3.zero;
        private InputSystem _input;

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
        }
        
        public void MovementInput(Vector3 movementInput)
        {
            _movementInput = movementInput;
        }

        public void MouseMovement(Vector3 mousePosition)
        {
            _mousePos = mousePosition;
        }

        public void FireButtonPressed()
        {
            // TODO: add shooting mechanics
            Debug.Log("Shoot");
        }

        // Do main actions here
        private void Update()
        {
            _aimingDirection = (_mousePos - transform.position).normalized;
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
            Handles.Label(pos, "FacingRight: " + movement.IsFacingRight);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(pos, _aimingDirection * 2);
        }
#endif

        #endregion
    }
}