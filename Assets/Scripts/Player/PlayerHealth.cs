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
        public float HealthPercent => (float)_currentHealth / _maxHealth.Value;
        
        public void ApplyDamage(int value)
        {
            _currentHealth -= value;
        }

        public void ApplyHeal(int value)
        {
            _currentHealth = Math.Clamp(_currentHealth + value, 0, MaxHealth);
        }

        public void LoadConfig(PlayerConfig config)
        {
            _maxHealth = new ModifiableValueInt(config.MaxHealth);
            _currentHealth = MaxHealth;
        }
    }
}