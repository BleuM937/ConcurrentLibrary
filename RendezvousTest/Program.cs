namespace RendezvousTest
{
    using System;
    using ConcurrentLibrary;
    using Thread = System.Threading.Thread;

    class Program
    {
        static void Main(string[] args)
        {
            Semaphore sem1 = new Semaphore(), sem2 = new Semaphore();

            new Thread(Rendezvous) { Name = "Thread 1" }.Start(Tuple.Create(sem1, sem2));
            new Thread(Rendezvous) { Name = "Thread 2" }.Start(Tuple.Create(sem2, sem1));
        }

        static void Rendezvous(object param)
        {
            var sems = param as Tuple<Semaphore, Semaphore>;
            var random = new Random();

            var thisReady = sems.Item1;
            var otherReady = sems.Item2;

            for (int i = 0; true; i++)
            {
                Console.WriteLine("{0}: Preparing to rendezvous {1}", Thread.CurrentThread.Name, i);
                Thread.Sleep(random.Next(2000, 3000));
                thisReady.Release();
                otherReady.Acquire();
                Console.WriteLine("{0}: Arrived at rendezvous {1}", Thread.CurrentThread.Name, i);
            }
        }
    }
}
