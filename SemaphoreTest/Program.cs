namespace SemaphoreTest
{
    using System;
    using Thread = System.Threading.Thread;
    using ConcurrentLibrary;

    class Program
    {
        static void Main(string[] args)
        {
            var semaphore = new Semaphore();

            new Thread(Producer) { Name = "Producer 1" }.Start(semaphore);
            new Thread(Consumer) { Name = "Consumer 1" }.Start(semaphore);
            new Thread(Consumer) { Name = "Consumer 2" }.Start(semaphore);
        }

        private static void Producer(object param)
        {
            var semaphore = param as Semaphore;

            Console.WriteLine("Starting:  {0}", Thread.CurrentThread.Name);

            while (true)
            {
                Console.WriteLine("Releasing: {0}", Thread.CurrentThread.Name);
                semaphore.Release();
                Console.WriteLine("Released:  {0}", Thread.CurrentThread.Name);
                Thread.Sleep(2000);
            }
        }

        private static void Consumer(object param)
        {
            var semaphore = param as Semaphore;

            Console.WriteLine("Starting:  {0}", Thread.CurrentThread.Name);

            while (true)
            {
                Console.WriteLine("Acquiring: {0}", Thread.CurrentThread.Name);
                semaphore.Acquire();
                Console.WriteLine("Acquired:  {0}", Thread.CurrentThread.Name);
            }
        }
    }
}
