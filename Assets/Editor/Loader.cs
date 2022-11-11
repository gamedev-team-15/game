using GameAssets;
using Player;
using UnityEngine;

namespace Editor
{
    public class Loader : MonoBehaviour
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
