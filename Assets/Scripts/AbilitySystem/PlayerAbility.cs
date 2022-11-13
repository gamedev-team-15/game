using Player;

namespace AbilitySystem
{
    public abstract class PlayerAbility : Ability
    {
        protected PlayerController Player { private set; get; }

        public void SetPlayer(PlayerController player)
        {
            Player = player;
        }
    }
}