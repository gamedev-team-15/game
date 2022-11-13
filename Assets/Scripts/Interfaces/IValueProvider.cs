namespace Interfaces
{
    public interface IValueProvider<out T>
    {
        public T GetValue();
    }
}