using Interfaces;
using UnityEngine;

namespace Utils
{
    public class ProxyVelocity : IValueProvider<Vector2>
    {
        private readonly Rigidbody2D _rb2d;
        public Vector2 Velocity => _rb2d.velocity;

        public ProxyVelocity(Rigidbody2D rb2d)
        {
            _rb2d = rb2d;
        }

        public Vector2 GetValue()
        {
            return _rb2d.velocity;
        }
    }
}