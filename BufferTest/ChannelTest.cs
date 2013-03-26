namespace ConcurrentLibrary.ChannelTest
{
    using System;
    using System.Threading;

    class ChannelTest
    {
        static void Main(string[] args)
        {
            Channel<string> channel = new Channel<string>();

            for (int i = 0; i < 2; i++)
            {
                new Thread((p) =>
                    {
                        int n = (int)p;
                        Random random = new Random();
                        for (int j = 0; true; j++)
                        {
                            string item = string.Concat(n, ":", j);
                            Console.WriteLine("{0} putting {1}", Thread.CurrentThread.Name, item);
                            channel.Put(item);
                            Thread.Sleep(random.Next(1500, 3000));
                        }
                    }) { Name = "Put " + i }.Start(i);
            }

            for (int i = 0; i < 4; i++)
            {
                new Thread(() =>
                    {
                        while (true)
                        {
                            Console.WriteLine("{0} taking", Thread.CurrentThread.Name);
                            string item = channel.Take();
                            Console.WriteLine("{0} took {1}", Thread.CurrentThread.Name, item);
                        }
                    }) { Name = "Take " + i }.Start();
            }
        }
    }
}
