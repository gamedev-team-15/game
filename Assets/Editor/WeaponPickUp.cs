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
            if (!weapon)
            {
                Destroy(gameObject);
                return;
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = weapon.Sprite;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.gameObject.TryGetComponent(out PlayerController c))
                c.Weapons.PickUpWeapon(weapon);
        }
    }
}
