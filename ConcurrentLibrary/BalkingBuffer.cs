namespace ConcurrentLibrary
{
    using System;
    using System.Collections.Generic;

    public class BalkingBuffer<T> : IBuffer<T>
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
            lock (_items)
            {
                if (_items.Count == 0)
                {
                    throw new InvalidOperationException(
                        "BalkingBuffer is empty.");
                }
                else
                {
                    T item = _items[0];
                    _items.RemoveAt(0);
                    return item;
                }
            }
        }
    }
}
