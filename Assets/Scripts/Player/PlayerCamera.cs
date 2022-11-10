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
        [SerializeField] private bool look;

        private void Start()
        {
            Camera = gameObject.GetComponent<Camera>();
        }

        private void FixedUpdate()
        {
            if(!target) return;
            transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed);

            if(look)
                transform.LookAt(target);
        }
    }
}