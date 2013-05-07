namespace ConcurrentLibrary
{
    using System;
    using System.Threading;

    /// <summary>
    /// Implements a strong semaphore. The strong semaphore guarantees that all
    /// threads waiting for the semaphore will make progress if the semaphore
    /// is being released. That is, threads have a bounded wait time when the
    /// number of threads waiting is finite.
    /// </summary>
    /// <remarks>
    /// This semaphore does not guarantee strict First-in, First-out (FIFO)
    /// behaviour. For a FIFO Semaphore use the <see cref="FifoSemaphore"/>
    /// class.
    /// </remarks>
    public class StrongSemaphore
    {
        // Protection for room1 and room2.
        private readonly object _lock = new object();
        // Number of free tokens.
        private volatile int free = 0;
        // Number of threads in room 1.
        private volatile int room1 = 0;
        // Number of threads in room 2.
        private volatile int room2 = 0;
        // A turnstile to leave room 1.
        private readonly Semaphore turnstile1 = new Semaphore(0);
        // A turnstile to leave room 2.
        private readonly Semaphore turnstile2 = new Semaphore(0);

        public void Acquire()
        {
            lock (_lock)
            {
                // If there are more tokens available than waiting,
                // use a token from the 'free' pool.
                if (free > 0)
                {
                    --free;
                    return;
                }

                // Enter room 1.
                ++room1;
            }
            // Wait to leave room 1.
            turnstile1.Acquire();
            lock (_lock)
            {
                // Enter room 2.
                ++room2;
                --room1;
                // If all threads have moved from room 1 into room 2,
                // get ready to leave room 1. Otherwise, let another
                // thread enter room 1.
                if (room1 == 0) turnstile2.Release();
                else turnstile1.Release();
            }
            // Wait to leave room 2.
            turnstile2.Acquire();
            lock (_lock) --room2;
        }

        public void Release()
        {
            // If room 2 is empty, allow a thread to move from room 1 into 
            // room 2. Otherwise, a thread in room 2 can continue.
            lock (_lock)
            {
                if (room2 == 0)
                {
                    if (room1 == 0) ++free;
                    else turnstile1.Release();
                }
                else turnstile2.Release();
            }
        }
    }
}
