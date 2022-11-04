using UnityEngine;

namespace Input
{
    public interface IInputSystemListener
    {
        public void MovementInput(Vector3 movementInput);
        public void MouseMovement(Vector3 mousePosition);
        public void FireButtonPressed();
    }
}