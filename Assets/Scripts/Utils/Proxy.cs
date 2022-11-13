using Interfaces;

namespace Utils
{
    public class Proxy<T>
    {
        private readonly IValueProvider<T> _provider;

        public Proxy(IValueProvider<T> provider)
        {
            _provider = provider;
        }

        public T Value => _provider.GetValue();
    }
}