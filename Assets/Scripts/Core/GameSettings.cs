using System;

namespace Core
{
    public static class GameSettings
    {
        private static float _generalVolume = 1f;
        private static float _musicVolume = 1f;
        private static float _fxVolume = 1f;

        public static float GeneralVolume
        {
            get => _generalVolume;
            set => _generalVolume = Math.Clamp(value, 0f, 1f);
        }

        public static float MusicVolume
        {
            get => _generalVolume * _musicVolume;
            set => _musicVolume = Math.Clamp(value, 0f, 1f);
        }
        
        public static float FxVolume
        {
            get => _generalVolume * _fxVolume;
            set => _fxVolume = Math.Clamp(value, 0f, 1f);
        }

    }
}