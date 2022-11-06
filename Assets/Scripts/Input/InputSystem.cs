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
        protected Vector3 InternalMousePosition = Vector3.zero;

        public Camera Camera { get; private set; }

        public Vector3 MovementInput => InternalMovementInput;
        public Vector3 MousePosition => InternalMousePosition;
        
        
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