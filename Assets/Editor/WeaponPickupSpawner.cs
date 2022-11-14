using GameAssets;
using UnityEngine;

namespace Editor
{
    public class WeaponPickupSpawner : MonoBehaviour
    {
        [SerializeField] private WeaponData[] weapons = {};
        [SerializeField] private float gap = 1f;
        [SerializeField] private WeaponPickUp pickUp;
        
        private void Start()
        {
            int i = 0;
            foreach (var weapon in weapons)
            {
                var pu = Instantiate(pickUp, transform.position - (Vector3.right * gap * (i++ - weapons.Length / 2)), transform.rotation);
                pu.transform.parent = transform;
                pu.SetWeapon(weapon);
            }
        }
    }
}
