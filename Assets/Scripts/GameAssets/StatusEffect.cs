using Modifications;
using UnityEngine;

namespace GameAssets
{
    [CreateAssetMenu(fileName = "new Status Effect", menuName = "Game Assets/Status Effect")]
    public class StatusEffect : GameAsset
    {
        [SerializeField] private StatModifier modifier;
        public StatModifier Modifier => modifier;
        public bool Stackable => modifier.Stackable;
    }
}