namespace ConcurrentLibrary
{
    using System;
    using System.Collections.Generic;

    public class BusyBuffer<T> : IBuffer<T>
    {
        private List<T> _items = new List<T>();

        public void Put(T data)
        {
            lock (_items)
            {
                _items.Add(data);
            }
        }

        public T Take()
        {
            // TODO: How to implement busy wait with locking
            throw new NotImplementedException();
        }
    }
}
