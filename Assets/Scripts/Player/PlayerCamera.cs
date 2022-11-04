using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Camera))]
    public class PlayerCamera : MonoBehaviour
    {
        private Transform _player;
        public Camera Camera { get; private set; }

        private void Start()
        {
            _player = FindObjectOfType<PlayerController>().transform;
            Camera = gameObject.GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            if (!_player) return;
            // TODO: add camera smoothing
            var pos = _player.position;
            pos.z = transform.position.z;
            transform.position = pos;
        }
    }
}