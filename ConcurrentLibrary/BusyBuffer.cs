namespace ConcurrentLibrary
{
    using System;
    using System.Collections.Generic;

    public class BusyBuffer<T> : IBuffer<T>
    {
        private Queue<T> _items = new Queue<T>();
        public void Put(T data)
        {
            lock (_items)
            {
                _items.Enqueue(data);
            }
        }

        public T Take()
        {
            while (true)
            {
                lock (_items)
                {
                    if (_items.Count > 0)
                    {
                        return _items.Dequeue();
                    }
                }
            }
        }
    }
}
