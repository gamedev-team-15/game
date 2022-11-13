using UnityEngine;

namespace GameAssets
{
    public abstract class GameAsset : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;
        [SerializeField] private string description;
        public string Description => description;
    }
}