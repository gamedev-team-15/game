using System.Collections.Generic;
using UnityEngine;

namespace Input
{
    public class InputSystem : MonoBehaviour
    {
        protected readonly HashSet<IInputSystemListener> Listeners = new();
        private bool _isInitialized;
        protected bool IsInitialized => _isInitialized;
        protected Vector3 InternalMovementInput = Vector3.zero;
        protected Vector3 InternalCrosshairPosition = Vector3.zero;
        protected Vector3 InternalAimingDirection = Vector3.right;

        public Camera Camera { get; private set; }

        public Vector3 MovementInput => InternalMovementInput;
        public Vector3 CrosshairPosition => InternalCrosshairPosition;
        public Vector3 AimingDirection => InternalAimingDirection;
        public bool UsingMouse => UnityEngine.Input.mousePresent;
        
        
        public void AddInputListener(IInputSystemListener listener)
        {
            Listeners.Add(listener);
        }

        public void RemoveInputListener(IInputSystemListener listener)
        {
            Listeners.Remove(listener);
        }
        
        private void Awake()
        {
            Camera = Camera.main;
            _isInitialized = true;
        }
    }
}