using GameAssets;
using Player;
using UnityEngine;

namespace Core
{
    public class Bootloader : MonoBehaviour
    {
        [SerializeField] private PlayerConfig config;
        private void Awake()
        {
            var player = FindObjectOfType<PlayerController>();
            if(player)
                player.LoadConfig(config);
        }
    }
}
