using System.Collections.Generic;
using GameAssets;
using Interfaces;

namespace Modifications
{
    public abstract class StatSystem : IUpdatable, IEffect
    {
        private class StatModifierContainer : IUpdatable
        {
            public StatModifier Modifier { get; }
            private float _timer;
            public bool Ended => _timer <= 0;

            public StatModifierContainer(StatModifier modifier)
            {
                Modifier = modifier;
                _timer = modifier.Duration;
            }

            public override bool Equals(object obj)
            {
                return obj is StatModifierContainer con && Modifier.Equals(con.Modifier);
            }

            public override int GetHashCode()
            {
                return Modifier.GetHashCode();
            }

            public void Update(float deltaTime)
            {
                _timer -= deltaTime;
            }
        }

        private readonly List<StatModifierContainer> _modifiers = new();

        public void Update(float deltaTime)
        {
            _modifiers.RemoveAll(val =>
            {
                val.Update(deltaTime);
                if (val.Modifier.Infinite || !val.Ended) return false;
                Remove(val.Modifier);
                return true;
            });
        }

        public void ApplyEffect(StatusEffect effect)
        {
            var container = new StatModifierContainer(effect.Modifier);
            if(!effect.Stackable && _modifiers.Contains(container)) return;
            _modifiers.Add(new StatModifierContainer(effect.Modifier));
            Apply(effect.Modifier);
        }

        protected abstract void Apply(StatModifier modifier);
        protected abstract void Remove(StatModifier modifier);
    }
}