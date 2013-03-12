namespace ConcurrentLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class WaitBuffer<T> : IBuffer<T>
    {
        private List<T> _items = new List<T>();

        public void Put(T data)
        {
            lock (_items)
            {
                _items.Add(data);
                Monitor.PulseAll(_items);
            }
        }

        public T Take()
        {
            lock (_items)
            {
                while (_items.Count == 0) Monitor.Wait(_items);
                T item = _items[0];
                _items.RemoveAt(0);
                return item;
            }
        }
    }
}
