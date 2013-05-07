using System;
using System.Threading;
using System.Collections.Generic;

namespace ConcurrentLibrary
{
    public class FifoSemaphore
    {
        private readonly object _lock = new object();
        private volatile int tokens = 0;
        private readonly Channel<Semaphore> waiting = new Channel<Semaphore>();

        public void Acquire()
        {
            //lock (_lock)
            //{
            //    --tokens;
            //    if (tokens >= 0) return;
            //}
            if (Interlocked.Decrement(ref tokens) >= 0) return;
            var sem = new Semaphore();
            waiting.Put(sem);
            sem.Acquire();
        }

        public void Release(int count = 1)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException("count",
                    "Count must be greater than zero.");

            while (count --> 0)
            {
                //lock (_lock)
                //{
                //    ++tokens;
                //    if (tokens > 0) return;
                //}
                if (Interlocked.Increment(ref tokens) > 0) return;
                var sem = waiting.Take();
                sem.Release();
            }
        }
    }
}
