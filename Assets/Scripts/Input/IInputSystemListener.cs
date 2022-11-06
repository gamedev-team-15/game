using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Input
{
    public interface IInputSystemListener
    {
        public void FireButtonPressed();
        public void FireButtonReleased();
        // TODO: Add support for abilities/actions(interactions)
    }
}