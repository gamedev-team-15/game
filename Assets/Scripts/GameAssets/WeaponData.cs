using UnityEngine;

namespace GameAssets
{
    [CreateAssetMenu(fileName = "new Weapon", menuName = "Game Assets/Weapon")]
    public class WeaponData : GameAsset
    {
        [SerializeField] private Sprite sprite;
        public Sprite Sprite => sprite;
        [SerializeField] private Vector3 muzzleOffset;
        public Vector3 MuzzleOffset => muzzleOffset;
        [SerializeField] private Weapon.Weapon weapon;
        public Weapon.Weapon Weapon => weapon;
    }
}