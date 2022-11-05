using System.Collections.Generic;
using UnityEngine;

namespace Input
{
    public class InputSystem : MonoBehaviour
    {
        private readonly HashSet<IInputSystemListener> _listeners = new();
        private bool _isInitialized = false;
        private Vector3 _movementInput = Vector3.zero;
        private Vector3 _mousePosition = Vector3.zero;
        private Camera _camera;

        public void AddInputListener(IInputSystemListener listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveInputListener(IInputSystemListener listener)
        {
            _listeners.Remove(listener);
        }
        
        private void Awake()
        {
            _camera = Camera.main;
            _isInitialized = true;
        }

        public Vector3 MovementInput => _movementInput;
        public Vector3 MousePosition => _mousePosition;

        private void Update()
        {
            if(!_isInitialized) return;
            
            _movementInput.x = UnityEngine.Input.GetAxisRaw("Horizontal");
            _movementInput.y = UnityEngine.Input.GetAxisRaw("Vertical");
            _movementInput.Normalize();
            foreach (var l in _listeners)
            {
                l.MovementInput(_movementInput);
            }

            _mousePosition = UnityEngine.Input.mousePosition;
            _mousePosition.z = -_camera.transform.position.z;
            _mousePosition = _camera.ScreenToWorldPoint(_mousePosition);

            if(UnityEngine.Input.GetButtonDown("PrimaryFire"))
                foreach (var l in _listeners)
                    l.FireButtonPressed();
            else if(UnityEngine.Input.GetButtonUp("PrimaryFire"))
                foreach (var l in _listeners)
                    l.FireButtonReleased();
        }
    }
}