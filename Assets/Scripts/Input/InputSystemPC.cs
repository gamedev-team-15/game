using UnityEngine;

namespace Input
{
    public class InputSystemPC : InputSystem
    {
        private readonly string[] _abilityButtons = { "Ability1", "Ability2", "Ability3" };

        private void Update()
        {
            if (!IsInitialized) return;

            InternalMovementInput.x = UnityEngine.Input.GetAxisRaw("Horizontal");
            InternalMovementInput.y = UnityEngine.Input.GetAxisRaw("Vertical");
            InternalMovementInput.Normalize();

            InternalCrosshairPosition = UnityEngine.Input.mousePosition;
            InternalCrosshairPosition.z = -Camera.transform.position.z;
            InternalCrosshairPosition = Camera.ScreenToWorldPoint(InternalCrosshairPosition);

            var halfScreenDimension = new Vector3(Screen.width / 2f, Screen.height / 2f);
            InternalAimingDirection = (UnityEngine.Input.mousePosition - halfScreenDimension).normalized;

            for (int i = 0; i < _abilityButtons.Length; i++)
                if (UnityEngine.Input.GetButtonDown(_abilityButtons[i]))
                    foreach (var l in Listeners)
                        l.AbilityButtonPressed(i);

            if (UnityEngine.Input.GetButtonDown("Interact"))
                foreach (var l in Listeners)
                    l.Interact();

            if (UnityEngine.Input.GetButtonDown("PrimaryFire"))
                foreach (var l in Listeners)
                    l.FireButtonPressed();

            if (UnityEngine.Input.GetButtonUp("PrimaryFire"))
                foreach (var l in Listeners)
                    l.FireButtonReleased();
        }
    }
}