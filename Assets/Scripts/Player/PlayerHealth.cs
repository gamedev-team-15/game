using System;
using GameAssets;
using Interfaces;
using ModVal;

namespace Player
{
    public class PlayerHealth : IDamage, IHeal, IPlayerConfigLoader
    {
        private ModifiableValueInt _maxHealth = new(100);
        public int MaxHealth => _maxHealth.Value;
        private int _currentHealth = 100;
        public int CurrentHealth => _currentHealth;
        
        public void ApplyDamage(int damage)
        {
            _currentHealth -= damage;
        }

        public void ApplyHeal(int hp)
        {
            _currentHealth = Math.Clamp(_currentHealth + hp, 0, MaxHealth);
        }

        public void LoadConfig(PlayerConfig config)
        {
            _maxHealth = new ModifiableValueInt(config.MaxHealth);
            _currentHealth = MaxHealth;
        }
    }
}