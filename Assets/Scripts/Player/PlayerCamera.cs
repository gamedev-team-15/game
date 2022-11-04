using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Camera))]
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        public Camera Camera { get; private set; }
        [SerializeField] private Vector3 offset = new(0f, 0f, -10f);
        [SerializeField] [Min(0.001f)] private float smoothSpeed = 0.125f;

        private void Start()
        {
            Camera = gameObject.GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            if(!target) return;
            Vector3 targetPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target);
        }
    }
}