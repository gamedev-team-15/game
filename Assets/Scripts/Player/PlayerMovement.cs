using System;
using ModVal;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerMovement
    {
        #region Varibles

        public bool IsFacingRight { get; private set; } = true;
        public Rigidbody2D Rb2D { get; private set; }
        [SerializeField] private ModifiableValueFloat speed = new(5);
        public ModifiableValueFloat Speed => speed;

        #endregion
        
        // TODO: add movement abilities (dash, blink, etc.)

        #region Methods

        public void SetRigidbody(Rigidbody2D rb2d)
        {
            Rb2D = rb2d ? rb2d : throw new ArgumentNullException(nameof(rb2d), "Rigidbody cannot be null");
        }

        public void Move(Vector2 direction)
        {
            Rb2D.AddForce(direction * (speed.Value * Time.deltaTime * Rb2D.drag * 50));
            Rb2D.velocity = Vector2.ClampMagnitude(Rb2D.velocity, speed.Value * 10);
            IsFacingRight = Math.Abs(direction.x) > 0.1f ? direction.x > 0 : IsFacingRight;
        }

        #endregion
    }
}