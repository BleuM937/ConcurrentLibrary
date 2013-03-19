namespace ConcurrentLibrary.BarrierTest
{
    using System;
    using Thread = System.Threading.Thread;
    using ConcurrentLibrary;

    class Program
    {
        static Barrier barrier;
        static void Main(string[] args)
        {
            barrier = new Barrier(4);
            for (int i = 0; i < 4; i++)
            {
                new Thread(() =>
                    {
                        Random random = new Random();
                        for (int j = 0; true; j++)
                        {
                            Thread.Sleep(random.Next(1500, 4000));
                            Console.WriteLine("{0} waiting on barrier. [{1}]",
                                Thread.CurrentThread.Name, j);
                            barrier.Arrive();
                            Console.WriteLine("{0} passed barrier. [{1}]",
                                Thread.CurrentThread.Name, j);
                        }

                    }) { Name = "Thread " + i.ToString() }.Start();
            }
        }
    }
}
