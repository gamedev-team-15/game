using System.Collections.Generic;
using UnityEngine;

namespace ModVal
{
    [System.Serializable]
    public abstract class ModifiableValue<T>
    {
        [SerializeField] private T baseValue;
        protected T CurrentValue;
        public T Value => CurrentValue;
        public T BaseValue => baseValue;
        protected readonly List<ValueModifier<T>> Modifiers;

        protected ModifiableValue(T baseValue)
        {
            this.baseValue = baseValue;
            Modifiers = new List<ValueModifier<T>>();
            UpdateValue();
        }

        public void AddModifier(ValueModifier<T> modifier)
        {
            Modifiers.Add(modifier);
            UpdateValue();
        }

        public bool HasModifier(ValueModifier<T> modifier)
        {
            return Modifiers.Contains(modifier);
        }
        
        public void RemoveModifier(ValueModifier<T> modifier)
        {
            Modifiers.Remove(modifier);
            UpdateValue();
        }
        
        public void RemoveLastModifier(ValueModifier<T> modifier)
        {
            int index = Modifiers.LastIndexOf(modifier);
            if(index > 0)
                Modifiers.RemoveAt(index);
            UpdateValue();
        }

        public void RemoveLastModifier()
        {
            int index = Modifiers.Count - 1;
            if(index > 0)
                Modifiers.RemoveAt(index);
            UpdateValue();
        }

        public void ClearModifiers()
        {
            Modifiers.Clear();
            CurrentValue = baseValue;
        }

        public override string ToString()
        {
            return $"ModVal({typeof(T)}){{base: {baseValue}, current: {CurrentValue}}}";
        }

        protected void UpdateValue()
        {
            CurrentValue = BaseValue;
            foreach (var modifier in Modifiers)
            {
                switch (modifier.Type)
                {
                    case ModifierType.Percentage:
                        CurrentValue = Add(CurrentValue, Divide(Multiply(CurrentValue, modifier.Value), 100.0f));
                        break;
                    case ModifierType.BasePercentage:
                        CurrentValue = Add(CurrentValue, Divide(Multiply(BaseValue, modifier.Value), 100.0f));
                        break;
                    case ModifierType.Constant:
                    default:
                        CurrentValue = Add(CurrentValue, modifier.Value);
                        break;
                }
            }
        }

        protected abstract T Add(T a, T b);

        protected abstract T Multiply(T a, T b);

        protected abstract T Divide(T a, float b);
    }
}