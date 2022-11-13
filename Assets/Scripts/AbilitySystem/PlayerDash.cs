using System;
using UnityEngine;

namespace AbilitySystem
{
    [Serializable]
    public class PlayerDash : PlayerAbility
    {
        [SerializeField] private int force = 10;

        public override void Activate()
        {
            Player.Movement.Rb2D.AddForce(Player.Movement.Rb2D.velocity.normalized * force, ForceMode2D.Impulse);
        }
    }
}