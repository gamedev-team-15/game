using System;
using UnityEngine;

namespace HealthSystem
{
    [Serializable]
    public class Health : Interfaces.IDamage, Interfaces.IHeal
    {
        private int _health;
        [SerializeField] private int maxHealth;
        public float HealthPercent => (float)_health / maxHealth;
        public int CurrentHealth => _health;
        public int MaxHealth => maxHealth;

        public Health(int maxHp)
        {
            _health = maxHealth = maxHp;
        }

        public void SetHealth(int hp)
        {
            _health = Math.Clamp(hp, 0, maxHealth);
        }
        public void ApplyDamage(int value)
        {
            _health -= value;
        }

        public void ApplyHeal(int value)
        {
            _health = Math.Min(maxHealth, _health + value);
        }
    }
}