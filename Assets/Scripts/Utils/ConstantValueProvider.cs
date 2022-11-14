using Interfaces;

namespace Utils
{
    public class ConstantValueProvider<T> : IValueProvider<T>
    {
        private readonly T _value;

        public ConstantValueProvider(T value)
        {
            _value = value;
        }

        public T GetValue()
        {
            return _value;
        }
    }
}