namespace ConcurrentLibrary
{
    using System;
    using System.Threading;

    public class Semaphore
    {
        private readonly object tokenLock = new object();
        private int tokens;

        public Semaphore(int tokens = 0)
        {
            this.tokens = tokens;
        }

        public void Acquire()
        {
            lock (tokenLock)
            {
                while (tokens == 0)
                    Monitor.Wait(tokenLock);
                --tokens;
            }
        }

        public void Release()
        {
            lock (tokenLock)
            {
                ++tokens;
                Monitor.PulseAll(tokenLock);
            }
        }
    }
}
