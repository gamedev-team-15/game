using System;
using UnityEngine;

namespace AbilitySystem
{
    [Serializable]
    public abstract class Ability
    {
        [SerializeField] private float cooldown;
        public float Cooldown => cooldown;

        public abstract void Activate();
    }
}