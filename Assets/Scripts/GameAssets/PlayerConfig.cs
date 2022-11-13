using System.Collections.Generic;
using UnityEngine;

namespace GameAssets
{
    [CreateAssetMenu(fileName = "new Player Config", menuName = "Game Assets/Player/Player Config")]
    public class PlayerConfig : GameAsset
    {
        [SerializeField] private int speed = 300;
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private PlayerAbilityData[] abilities = {};
        [SerializeField] private WeaponData defaultWeapon;
        // TODO: mana(stamina)

        public int Speed => speed;
        public int MaxHealth => maxHealth;
        public WeaponData DefaultWeapon => defaultWeapon;
        public IEnumerable<PlayerAbilityData> Abilities => abilities;
    }
}