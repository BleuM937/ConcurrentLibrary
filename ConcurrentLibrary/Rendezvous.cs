namespace ConcurrentLibrary
{
    public class Rendezvous<T>
    {
        private readonly object exchangeLock = new object();
        Semaphore ready = new Semaphore();
        Semaphore turnstile = new Semaphore(1);
        T data;
        bool filled;

        public T Exchange(T item)
        {
            T result;
            turnstile.Acquire();
            lock (exchangeLock)
            {
                if (filled)
                {
                    result = data;
                    data = item;
                    ready.Release();
                    return result;
                }
                else
                {
                    data = item;
                    filled = true;
                    turnstile.Release();
                }
            }
            ready.Acquire();
            lock (exchangeLock)
            {
                filled = false;
                result = data;
            }
            turnstile.Release();
            return result;
        }
    }
}
