using System;
using System.Collections.Generic;
using GameAssets;
using Interfaces;

namespace Modifications
{
    public abstract class StatSystem : IUpdatable, IEffect
    {
        public class StatModifierContainer : IUpdatable
        {
            public StatusEffect Effect { get; }
            private float _timer;
            public bool Ended => _timer <= 0;
            public float RemainingTime => Math.Max(0, _timer / Effect.Modifier.Duration);

            public StatModifierContainer(StatusEffect effect)
            {
                Effect = effect;
                _timer = effect.Modifier.Duration;
            }

            public override bool Equals(object obj)
            {
                return obj is StatModifierContainer con && Effect.Equals(con.Effect);
            }

            public override int GetHashCode()
            {
                return Effect.GetHashCode();
            }

            public void Update(float deltaTime)
            {
                _timer -= deltaTime;
            }

            public void Reset()
            {
                _timer = Effect.Modifier.Duration;
            }
        }

        private readonly List<StatModifierContainer> _modifiers = new();
        public IEnumerable<StatModifierContainer> Modifiers => _modifiers;

        public void Update(float deltaTime)
        {
            _modifiers.RemoveAll(val =>
            {
                val.Update(deltaTime);
                if (val.Effect.Modifier.Infinite || !val.Ended) return false;
                Remove(val.Effect.Modifier);
                return true;
            });
        }

        public void ApplyEffect(StatusEffect effect)
        {
            var container = new StatModifierContainer(effect);
            if (!effect.Stackable)
            {
                int mod = _modifiers.IndexOf(container);
                if (mod > -1)
                {
                    _modifiers[mod].Reset();
                    return;
                }
            }
            _modifiers.Add(container);
            Apply(effect.Modifier);
        }

        protected abstract void Apply(StatModifier modifier);
        protected abstract void Remove(StatModifier modifier);
    }
}