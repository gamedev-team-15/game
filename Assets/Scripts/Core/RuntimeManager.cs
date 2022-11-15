using Player;
using UnityEngine;

namespace Core
{
    public static class RuntimeManager
    {
        public static Utils.Utils Utils { private set; get; }
        private static bool _testMode = true;
        private static PlayerController _player;

        public static void Initialize(bool testMode)
        {
            _testMode = testMode;
            Utils = new Utils.Utils();
            _player = Object.FindObjectOfType<PlayerController>();
            if (_testMode)
                _player.Events.OnPlayerDeath.AddListener(TestModePlayerDeathHandler);
            else
                _player.Events.OnPlayerDeath.AddListener(PlayerDeathHandler);
        }

        private static void TestModePlayerDeathHandler()
        {
            var activeConfig = Object.FindObjectOfType<Bootloader>().LoadedPlayerPlayerConfig;
            _player.transform.position = Vector3.zero;
            _player.LoadConfig(activeConfig);
        }

        private static void PlayerDeathHandler()
        {
            // TODO: create player death handler for non-test game mode
            throw new System.NotImplementedException();
        }
    }
}
