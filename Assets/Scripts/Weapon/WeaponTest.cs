using UnityEngine;

namespace Weapon
{
    public class WeaponTest: MonoBehaviour
    {
        public float timeToSwitch;
        
        [SerializeField] private Weapon weapon;

        void Update()
        {
            timeToSwitch -= Time.deltaTime;
            weapon.Shoot(Vector2.right);
        }

        
    }
}

