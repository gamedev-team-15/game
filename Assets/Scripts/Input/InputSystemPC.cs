using UnityEngine;

namespace Input
{
    public class InputSystemPC : InputSystem
    {
        private void Update()
        {
            if(!IsInitialized) return;

            InternalMovementInput.x = UnityEngine.Input.GetAxisRaw("Horizontal");
            InternalMovementInput.y = UnityEngine.Input.GetAxisRaw("Vertical");
            InternalMovementInput.Normalize();

            InternalMousePosition = UnityEngine.Input.mousePosition;
            InternalMousePosition.z = -Camera.transform.position.z;
            InternalMousePosition = Camera.ScreenToWorldPoint(InternalMousePosition);

            if(UnityEngine.Input.GetButtonDown("PrimaryFire"))
                foreach (var l in Listeners)
                    l.FireButtonPressed();
            else if(UnityEngine.Input.GetButtonUp("PrimaryFire"))
                foreach (var l in Listeners)
                    l.FireButtonReleased();
        }
    }
}