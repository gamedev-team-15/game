using System;
using HealthSystem;
using UnityEngine;

public class TestTargets : MonoBehaviour
{
    [SerializeField] private HealthComponent[] targets = { };

    private void Start()
    {
        foreach (var target in targets)
        {
            target.gameObject.SetActive(true);
            target.OnDeath.AddListener((() => target.gameObject.SetActive(false)));
        }
    }

    public void Reset()
    {
        foreach (var target in targets)
        {
            target.gameObject.SetActive(true);
            target.ApplyHeal(target.Health.MaxHealth);
        }
    }
}
