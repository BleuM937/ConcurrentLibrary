namespace ConcurrentLibrary
{
    using System;

    public class BoundedChannel<T> : IBuffer<T>
    {
        private Semaphore spacesAvailable;
        private Channel<T> channel;

        public BoundedChannel(int capacity)
        {
            if (capacity < 1) throw new ArgumentOutOfRangeException("capacity",
                "Capacity must be greater than zero.");

            spacesAvailable = new Semaphore(capacity);
            channel = new Channel<T>();
        }

        public void Put(T data)
        {
            spacesAvailable.Acquire();
            channel.Put(data);
        }

        public T Take()
        {
            T item = channel.Take();
            spacesAvailable.Release();
            return item;
        }
    }
}
