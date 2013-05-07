using System;
using System.Threading;

namespace StrongSemaphoreTest
{
    using ConcurrentLibrary;

    class Program
    {
        static void Main(string[] args)
        {
            StrongSemaphore semaphore = new StrongSemaphore();

            new Thread(() =>
            {
                Random r = new Random();

                for (int i = 0; ; i++)
                {
                    new Thread(param =>
                    {
                        var sem = param as StrongSemaphore;
                        Console.WriteLine("Acquiring: " + Thread.CurrentThread.Name);
                        sem.Acquire();
                        Console.WriteLine("Acquired: " + Thread.CurrentThread.Name);
                    }) { Name = "Thread " + i }
                    .Start(semaphore);
                    Thread.Sleep(r.Next(500, 1000));
                }
            }).Start();

            while (true)
            {
                Console.ReadLine();
                Console.WriteLine(new String('-', 79));
                semaphore.Release();
            }
        }
    }
}
