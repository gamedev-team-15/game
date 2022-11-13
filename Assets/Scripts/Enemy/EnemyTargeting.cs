using UnityEngine;

namespace Enemy
{
    public class EnemyTargeting : MonoBehaviour
    {
        [SerializeField] public float speed = 3f;
        private Rigidbody2D _rb;
        private Vector2 _moveDirection;
        private Transform _target;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.drag = 1;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
                _target = other.transform;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
                _target = null;
        }

        private void Update()
        {
            if (!_target) return;
            var direction = (_target.position - transform.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rb.rotation = angle;
            _moveDirection = direction;
        }

        private void FixedUpdate()
        {
            if (_target)
                _rb.AddForce(new Vector2(_moveDirection.x, _moveDirection.y) * speed);
        }
    }
}