namespace ConcurrentLibrary
{
    public class LightSwitch
    {
        private readonly Semaphore target;
        private readonly object countLock = new object();
        private int count;

        public LightSwitch(Semaphore target) { this.target = target; }

        public void Acquire()
        {
            lock (countLock) 
                if (count++ == 0) target.Acquire();
        }

        public void Release()
        {
            lock (countLock)
                if (--count == 0) target.Release();
        }
    }
}
