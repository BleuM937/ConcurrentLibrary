namespace ConcurrentLibrary
{
    using System;
    
    public class Barrier
    {
        private readonly object barrierLock = new object();
        Semaphore turnstile = new Semaphore(1);
        Semaphore ready = new Semaphore(0);
        int numWaiting = 0;
        int maxWaiting;

        public Barrier(int waitCount) { maxWaiting = waitCount; }

        public void Arrive()
        {
            turnstile.Acquire();
            lock (barrierLock)
            {
                ++numWaiting;
                if (numWaiting == maxWaiting)
                    ready.Release(maxWaiting);
                turnstile.Release();
            }
            ready.Acquire();
            lock (barrierLock)
            {
                --numWaiting;
                if (numWaiting == 0)
                    turnstile.Release();
            }
        }
    }
}
