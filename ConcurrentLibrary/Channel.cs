using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConcurrentLibrary
{
    public class Channel<T> : IBuffer<T>
    {
        private Semaphore itemCount = new Semaphore(0);

        private readonly object itemLock = new object();
        private Queue<T> items = new Queue<T>();

        public void Put(T data)
        {
            lock (itemLock)
                items.Enqueue(data);
            itemCount.Release();
        }

        public T Take()
        {
            itemCount.Acquire();
            lock (itemLock)
                return items.Dequeue();
        }
    }
}
