using GameAssets;
using Player;
using UnityEngine;

namespace Editor
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class WeaponPickUp : MonoBehaviour
    {
        [SerializeField] private WeaponData weapon;

        private void Start()
        {
            SetWeapon(weapon);
        }

        public void SetWeapon(WeaponData weaponData)
        {
            weapon = weaponData;
            gameObject.GetComponent<SpriteRenderer>().sprite = weapon.Sprite;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!weapon) return;
            if(col.gameObject.TryGetComponent(out PlayerController c))
                c.Weapons.PickUpWeapon(weapon);
        }
    }
}
