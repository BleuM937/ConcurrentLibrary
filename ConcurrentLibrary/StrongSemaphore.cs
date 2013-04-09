namespace ConcurrentLibrary
{
    using System;
    using System.Threading;

    public class StrongSemaphore
    {
        private readonly object _lock = new object();
        int room1 = 0;
        int room2 = 0;
        Semaphore turnstile1 = new Semaphore(1);
        Semaphore turnstile2 = new Semaphore(0);

        public void Acquire()
        {
            lock (_lock) ++room1;
            turnstile1.Acquire();
            lock (_lock)
            {
                ++room2;
                --room1;
                if (room1 == 0) turnstile2.Release();
                else turnstile1.Release();
            }
            turnstile2.Acquire();
            --room2;
        }

        public void Release()
        {
            if (room2 == 0) turnstile1.Release();
            else turnstile2.Release();
        }
    }
}
