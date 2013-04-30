namespace ConcurrentLibrary
{
    public interface IPut<T>
    {
        void Put(T data);
    }

    public interface ITake<T>
    {
        T Take();
    }

    public interface IBuffer<T> : IPut<T>, ITake<T> { }
}
