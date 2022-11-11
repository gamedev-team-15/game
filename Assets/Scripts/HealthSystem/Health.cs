using System;
using UnityEngine;

namespace HealthSystem
{
    [Serializable]
    public class Health : Interfaces.IDamage, Interfaces.IHeal
    {
        private int health;
        [SerializeField]
        private int maxHealth;
        public float HealthPercent => (float)health / maxHealth;
        public int CurrentHealth => health;
        public int MaxHealth => maxHealth;

        public void SetHealth(int hp)
        {
            health = Math.Clamp(hp, 0, maxHealth);
        }
        public void ApplyDamage(int damage)
        {
            health -= damage;
        }

        public void ApplyHeal(int hp)
        {
            health = Math.Min(maxHealth, health + hp);
        }
    }
}