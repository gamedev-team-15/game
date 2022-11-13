using System;
using GameAssets;
using Interfaces;
using ModVal;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerMovement : IPlayerConfigLoader
    {
        #region Varibles

        public bool IsFacingRight { get; private set; } = true;
        public Rigidbody2D Rb2D { get; private set; }
        [SerializeField] private ModifiableValueInt speed = new(300);
        public ModifiableValueInt Speed => speed;

        #endregion

        #region Methods

        public void SetRigidbody(Rigidbody2D rb2d)
        {
            Rb2D = rb2d ? rb2d : throw new ArgumentNullException(nameof(rb2d), "Rigidbody cannot be null");
            speed.ClearModifiers();
        }

        public void Move(Vector2 direction)
        {
            Rb2D.AddForce(direction * (speed.Value * Time.deltaTime * Rb2D.drag));
            Rb2D.velocity = Vector2.ClampMagnitude(Rb2D.velocity, speed.Value * 10);
            IsFacingRight = Math.Abs(direction.x) > 0.1f ? direction.x > 0 : IsFacingRight;
        }

        #endregion

        public void LoadConfig(PlayerConfig config)
        {
            speed.ClearModifiers();
            speed = new ModifiableValueInt(config.Speed);
            speed.ClearModifiers();
        }
    }
}