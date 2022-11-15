using System;
using Modifications;

namespace Player
{
    public class PlayerStats : StatSystem
    {
        private PlayerController _player;

        public void SetPlayer(PlayerController player)
        {
            _player = player;
        }

        protected override void Apply(StatModifier modifier)
        {
            switch (modifier.Type)
            {
                case StatType.Movement:
                    _player.Movement.Speed.AddModifier(modifier.ValueModifier);
                    break;
                case StatType.MaxHealth:
                    break;
                case StatType.ProjectileDamage:
                    _player.Weapons.DamageModifiers.Add(modifier.ValueModifier);
                    break;
                case StatType.ProjectileCount:
                    break;
                case StatType.FireRate:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void Remove(StatModifier modifier)
        {
            switch (modifier.Type)
            {
                case StatType.Movement:
                    _player.Movement.Speed.RemoveModifier(modifier.ValueModifier);
                    break;
                case StatType.MaxHealth:
                    break;
                case StatType.ProjectileDamage:
                    _player.Weapons.DamageModifiers.Remove(modifier.ValueModifier);
                    break;
                case StatType.ProjectileCount:
                    break;
                case StatType.FireRate:
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}