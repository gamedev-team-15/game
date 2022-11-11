using AbilitySystem;

namespace GameAssets
{
    public abstract class PlayerAbilityData : GameAsset
    {
        public PlayerAbility Ability => GetAbility();

        protected abstract PlayerAbility GetAbility();
    }
}