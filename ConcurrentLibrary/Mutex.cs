namespace ConcurrentLibrary
{
    using System;

    public class Mutex
    {
        private readonly Semaphore enter = new Semaphore(1);
        private bool entered;

        public void Acquire()
        {
            enter.Acquire();
            entered = true;
        }

        public void Release()
        {
            if (!entered) throw new 
                InvalidOperationException("Cannot Release a Mutex before it has been acquired.");
            entered = false;
            enter.Release();
        }
    }
}
