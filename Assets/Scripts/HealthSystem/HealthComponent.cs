using Interfaces;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace HealthSystem
{
    public class HealthComponent : MonoBehaviour, IDamage, IHeal
    {
        [SerializeField] private Health health = new(100);
        [SerializeField] private UnityEvent onDamage;
        [SerializeField] private UnityEvent onHeal;
        [SerializeField] private UnityEvent onDeath;

        public Health Health => health;
        public UnityEvent OnDamage => onDamage;
        public UnityEvent OnHeal => onHeal;
        public UnityEvent OnDeath => onDeath;

        public void Start()
        {
            health.SetHealth(health.MaxHealth);
        }

        public void ApplyDamage(int damage)
        {
            onDamage.Invoke();
            health.ApplyDamage(damage);
            if (health.CurrentHealth <= 0)
            {
                onDeath.Invoke();
            }
        }

        public void ApplyHeal(int hp)
        {
            onHeal.Invoke();
            health.ApplyHeal(hp);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var pos = transform.position;
            GUI.color = Gizmos.color;
            Handles.Label(pos,
                $"Health: {health.CurrentHealth}/{health.MaxHealth}");
        }
#endif
    }
}