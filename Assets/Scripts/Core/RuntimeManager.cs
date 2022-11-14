using Player;
using UnityEngine;

namespace Core
{
    public class RuntimeManager : MonoBehaviour
    {
        public static RuntimeManager Instance { get; private set; }
        public static Utils.Utils Utils { private set; get; }
        [SerializeField] private bool editMode = true;
        private PlayerController _player;

        private void Start()
        {
            if (Instance)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            Utils = new Utils.Utils();
            _player = FindObjectOfType<PlayerController>();
            _player.Events.OnPlayerDeath.AddListener(PlayerDeathHandler);
        }

        private void PlayerDeathHandler()
        {
            if (editMode)
            {
                var activeConfig = FindObjectOfType<Bootloader>().LoadedPlayerConfig;
                _player.transform.position = Vector3.zero;
                _player.LoadConfig(activeConfig);
            }
        }
    }
}
