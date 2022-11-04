using UnityEditor;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        [SerializeField] private PlayerMovement movement = new();
        public PlayerMovement Movement => movement;
        private Vector2 _movementInput = Vector2.zero;
        private Vector2 _aimingDirection = Vector2.right;
        private Vector3 _mousePos = Vector3.zero;
        private Camera _camera;

        #endregion

        // TODO: add aiming and weapons, add health system, effects that modify player

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

            // Set up player camera
            _camera = FindObjectOfType<PlayerCamera>().Camera;
        }

        // Get all input and do other actions here
        private void Update()
        {
            // Get player movement input
            _movementInput.x = Input.GetAxisRaw("Horizontal");
            _movementInput.y = Input.GetAxisRaw("Vertical");
            _movementInput.Normalize();

            // Get player mouse location and calculate aiming direction
            _mousePos = Input.mousePosition;
            _mousePos.z = -_camera.transform.position.z;
            _aimingDirection = (_camera.ScreenToWorldPoint(_mousePos) - transform.position).normalized;

            // Shoot projectile
            if (Input.GetButtonDown("PrimaryFire"))
            {
                // Shoot
                Debug.Log("Shoot");
            }
        }

        // Process physics-related stuff
        private void FixedUpdate()
        {
            movement.Move(_movementInput);
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