using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Input
{
    public interface IInputSystemListener
    {
        public void MovementInput(Vector3 movementInput);
        public void FireButtonPressed();
        public void FireButtonReleased();
    }
}