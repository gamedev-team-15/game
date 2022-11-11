using AbilitySystem;
using UnityEngine;

namespace GameAssets
{
    [CreateAssetMenu(fileName = "new Player Dash", menuName = "Game Assets/Player/Player Dash")]
    public class PlayerDashData : PlayerAbilityData
    {
        [SerializeField] private PlayerDash dash;
        
        protected override PlayerAbility GetAbility()
        {
            return dash;
        }
    }
}