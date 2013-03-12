namespace ConcurrentLibrary
{
    public class Latch
    {
        private Semaphore ready = new Semaphore();

        public void Acquire()
        {
            ready.Acquire();
            ready.Release();
        }

        public void Release()
        {
            ready.Release();
        }
    }
}
