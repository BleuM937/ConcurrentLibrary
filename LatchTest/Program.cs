namespace ConcurrentLibrary.LatchTest
{
    using System;
    using System.Threading;
    using ConcurrentLibrary;

    class Program
    {
        static void Main(string[] args)
        {
            Latch latch = new Latch();

            ParameterizedThreadStart start = (param) =>
                {
                    int i = (int)param;
                    Console.WriteLine("Thread {0} acquiring latch", i);
                    latch.Acquire();
                    Console.WriteLine("Thread {0} continuing", i);
                };

            for (int i = 0; i < 3; i++)
            {
                new Thread(start).Start(i);
                Thread.Sleep(1000);
            }
            Console.WriteLine("Releasing latch");
            latch.Release();
            for (int i = 0; i < 2; i++)
            {
                new Thread(start).Start(i);
                Thread.Sleep(1000);
            }
            Console.ReadKey();
        }
    }
}
