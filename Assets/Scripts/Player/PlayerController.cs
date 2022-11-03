using System;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerMovement movement = new();
        public PlayerMovement Movement => movement;
        private Vector2 _input = Vector2.zero;
        // TODO: add aiming and weapons, add health system, effects that modify player

        private void Start()
        {
            if (!gameObject.TryGetComponent(out Rigidbody2D rb2d))
                rb2d = gameObject.AddComponent<Rigidbody2D>();
            rb2d.gravityScale = 0;
            rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            movement.SetRigidbody(rb2d);
        }
        
        // Get all input and other actions here
        private void Update()
        {
            _input.x = Input.GetAxisRaw("Horizontal");
            _input.y = Input.GetAxisRaw("Vertical");
            _input.Normalize();
            
        }
        
        // Process physics-related stuff
        private void FixedUpdate()
        {
            movement.Move(_input);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Vector3 pos = transform.position;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(pos, _input);
            GUI.color = Gizmos.color;
            Handles.Label(pos, "FacingRight: " + movement.IsFacingRight);
        }
#endif
    }
}