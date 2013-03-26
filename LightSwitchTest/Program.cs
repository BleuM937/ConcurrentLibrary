namespace ConcurrentLibrary.LightSwitchTest
{
    using System;
    using Thread = System.Threading.Thread;
    using ConcurrentLibrary;

    class Program
    {
        static void Main(string[] args)
        {
            Semaphore semaphore = new Semaphore(1);
            LightSwitch lightSwitch = new LightSwitch(semaphore);

            for (int i = 0; i < 2; i++)
            {
                new Thread(() =>
                    {
                        while (true)
                        {
                            Random random = new Random();
                            Console.WriteLine("{0} acquiring semaphore", Thread.CurrentThread.Name);
                            semaphore.Acquire();
                            Console.WriteLine("{0} acquired semaphore", Thread.CurrentThread.Name);
                            Thread.Sleep(random.Next(1500, 3000));
                            Console.WriteLine("{0} releasing semaphore", Thread.CurrentThread.Name);
                            semaphore.Release();
                            Thread.Sleep(random.Next(2000, 5000));
                        }
                    }) { Name = "Sem " + i }.Start();
            }

            for (int i = 0; i < 4; i++)
            {
                new Thread(() =>
                    {
                        while (true)
                        {
                            Random random = new Random();
                            Console.WriteLine("{0} acquiring lightswitch", Thread.CurrentThread.Name);
                            lightSwitch.Acquire();
                            Console.WriteLine("{0} acquired lightswitch", Thread.CurrentThread.Name);
                            Thread.Sleep(random.Next(500, 1000));
                            Console.WriteLine("{0} releasing lightswitch", Thread.CurrentThread.Name);
                            lightSwitch.Release();
                            Thread.Sleep(random.Next(500, 1500));
                        }
                    }) { Name = "LS " + i }.Start();
            }
        }
    }
}
