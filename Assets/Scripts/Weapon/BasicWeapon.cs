using UnityEngine;

namespace Weapon
{
    public class BasicWeapon : Weapon
    {
        private float cooldown;
        private float _timer = 0;
        private Transform _shotPoint;
    }
}

