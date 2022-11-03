using System;
using ModVal;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerMovement
    {
        public bool IsFacingRight { get; private set; } = true;
        public Rigidbody2D Rb2D { get; private set; }
        [SerializeField] private ModifiableValueFloat speed = new(5);
        public ModifiableValueFloat Speed => speed;
        [SerializeField] [Range(0, 1)] private float damping = 0.5f;
        // TODO: add movement abilities (dash, blink, etc.)

        public void SetRigidbody(Rigidbody2D rb2d)
        {
            Rb2D = rb2d ? rb2d : throw new ArgumentNullException(nameof(rb2d), "Rigidbody cannot be null");
        }
        
        public void Move(Vector2 direction)
        {
            Rb2D.velocity = Vector2.Lerp(Rb2D.velocity, direction * speed.Value, Time.deltaTime * (1 + damping * (Rb2D.velocity.sqrMagnitude > speed.Value * speed.Value ? 0.1f : 10)));
            IsFacingRight = Math.Abs(Rb2D.velocity.x) > 0.1f ? Rb2D.velocity.x > 0 : IsFacingRight;
        }
    }
}