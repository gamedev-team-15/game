using AbilitySystem;
using UnityEngine;

namespace GameAssets
{
    [CreateAssetMenu(fileName = "new Player Effect Ability", menuName = "Game Assets/Player/Player Effect Ability")]
    public class PlayerEffectAbilityData : PlayerAbilityData
    {
        [SerializeField] private PlayerEffectAbility ability;

        protected override PlayerAbility GetAbility()
        {
            return ability;
        }
    }
}