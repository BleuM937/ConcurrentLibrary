namespace ConcurrentLibrary.MutexTest
{
    using System;
    using Thread = System.Threading.Thread;
    using ConcurrentLibrary;

    class Program
    {
        static void Main(string[] args)
        {
            Mutex mutex = new Mutex();
            int acquireCount = 0;
            for (int i = 0; i < 4; i++)
            {
                new Thread(() =>
                    {
                        while (true)
                        {
                            Random random = new Random();
                            mutex.Acquire();
                            Console.WriteLine("\u250C {0} [{1}]", Thread.CurrentThread.Name, acquireCount++);
                            do
                            {
                                Console.WriteLine("\u2502 {0}", Thread.CurrentThread.Name);
                                Thread.Sleep(250);
                            } while (random.Next() % 4 != 0);
                            Console.WriteLine("\u2514 {0}", Thread.CurrentThread.Name);
                            mutex.Release();
                            Thread.Yield();
                        }
                    }) { Name = "Thread " + i.ToString() }.Start();
            }
        }
    }
}
