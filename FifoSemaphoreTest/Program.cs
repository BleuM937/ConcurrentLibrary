using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConcurrentLibrary.FifoSemaphoreTest
{
    class Program
    {
        static volatile int nextId = 0;
        static readonly FifoSemaphore semaphore = new FifoSemaphore();

        static void Main(string[] args)
        {
            while (true)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow: MakeRelease(); break;
                    case ConsoleKey.DownArrow: MakeAcquire(); break;
                    default: break;
                }
            }
        }

        static void MakeAcquire()
        {
            new Thread(param =>
            {
                var sem = param as FifoSemaphore;
                Console.WriteLine("Acquiring: " + Thread.CurrentThread.Name);
                sem.Acquire();
                Console.WriteLine("Acquired:  " + Thread.CurrentThread.Name);
            }) { Name = "Thread " + Interlocked.Increment(ref nextId) }
            .Start(semaphore);
        }

        static void MakeRelease()
        {
            Console.WriteLine(new String('-', 79));
            semaphore.Release();
        }
    }
}
