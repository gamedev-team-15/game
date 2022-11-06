using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModVal
{
    [Serializable]
    public struct ValueModifier<T> : IEquatable<ValueModifier<T>>
    {
        [SerializeField] private T value;
        [SerializeField] private ModifierType modifierType;
        public T Value => value;
        public ModifierType Type => modifierType;

        public ValueModifier(T value, ModifierType modifierType = ModifierType.Constant)
        {
            this.value = value;
            this.modifierType = modifierType;
        }

        public bool Equals(ValueModifier<T> other)
        {
            return EqualityComparer<T>.Default.Equals(value, other.value) && modifierType == other.modifierType;
        }

        public override bool Equals(object obj)
        {
            return obj is ValueModifier<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(value, (int)modifierType);
        }
    }
}