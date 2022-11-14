using System;
using ModVal;
using UnityEngine;

namespace Modifications
{
    [Serializable]
    public struct StatModifier
    {
        [SerializeField] private int value;
        [SerializeField] private StatType type;
        [SerializeField] private ModifierType modifierType;
        [SerializeField] private float duration;
        [SerializeField] private bool stackable;

        public int Value => value;
        public StatType Type => type;
        public float Duration => duration;
        public bool Infinite => duration < 0;
        public bool Stackable => stackable;
        public ValueModifier<int> ValueModifier => new(value, modifierType);
    }
}