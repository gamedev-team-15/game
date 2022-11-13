using System;
using GameAssets;
using UnityEngine;

namespace AbilitySystem
{
    [Serializable]
    public class PlayerEffectAbility : PlayerAbility
    {
        [SerializeField] private StatusEffect effect;
        public StatusEffect Effect => effect;

        public override void Activate()
        {
            Player.ApplyEffect(effect);
        }
    }
}