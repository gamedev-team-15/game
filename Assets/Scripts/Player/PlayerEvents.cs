using UnityEngine.Events;

namespace Player
{
    public class PlayerEvents
    {
        public UnityEvent OnPlayerDeath { get; } = new();
        public UnityEvent OnPlayerInteract { get; } = new();
    }
}