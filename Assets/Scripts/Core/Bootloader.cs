using GameAssets;
using Player;
using UnityEngine;

namespace Core
{
    public class Bootloader : MonoBehaviour
    {
        [SerializeField] private PlayerConfig playerConfig;
        public PlayerConfig LoadedPlayerPlayerConfig => playerConfig;
        [SerializeField] private bool testMode = true;
        
        public static Bootloader Instance { private set; get; }
        
        private void Awake()
        {
            if (Instance)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            RuntimeManager.Initialize(testMode);
            var player = FindObjectOfType<PlayerController>();
            if(player)
                player.LoadConfig(playerConfig);
        }
    }
}
