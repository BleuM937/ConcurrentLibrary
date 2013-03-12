using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConcurrentLibrary
{
    public class Channel<T> : IBuffer<T>
    {
        private Semaphore itemCount = new Semaphore(0);
        private List<T> items = new List<T>();

        public void Put(T data)
        {
            lock (items)
            {
                items.Add(data);
            }
            itemCount.Release();
        }

        public T Take()
        {
            itemCount.Acquire();
            lock (items)
            {
                var item = items[0];
                items.RemoveAt(0);
                return item;
            }
        }
    }
}
