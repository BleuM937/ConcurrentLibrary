namespace ConcurrentLibrary
{
    public interface IBuffer<T>
    {
        void Put(T data);
        T Take();
    }
}
