using Modifications;

namespace Interfaces
{
    public interface IModifiable
    {
        public void ApplyModifier(StatModifier modifier);
    }
}