using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BoundedChannelTest
{
    using ConcurrentLibrary;

    class Program
    {
        private static readonly BoundedChannel<string> _channel
            = new BoundedChannel<string>(4);

        static void Main(string[] args)
        {
            //for (int i = 0; i < 2; i++)
            int i = 0;
            new Thread(Producer).Start(i);
            //for (int i = 0; i < 2; i++)
            new Thread(Consumer).Start(i);
        }

        static void Producer(object param)
        {
            int n = (int)param;

            for (int i = 0; true; i++)
            {
                if (i % 3 == 0) Thread.Sleep(4000);
                else Thread.Sleep(1000);
                string msg = n + ":" + i;
                WriteLine(ConsoleColor.DarkBlue, "Producer {0}  -->", n);
                _channel.Put(msg);
                WriteLine(ConsoleColor.Blue, "Producer {0}  -----> {1}", n, msg);
            }
        }

        static void Consumer(object param)
        {
            int n = (int)param;

            for (int i = 0; true; i++)
            {
                if (i % 5 == 0) Thread.Sleep(5000);
                else Thread.Sleep(500);
                WriteLine(ConsoleColor.DarkYellow, "Consumer {0} <--", n);
                var msg = _channel.Take();
                WriteLine(ConsoleColor.Yellow, "Consumer {0} <----- {1}", n, msg);
            }
        }

        private static readonly object consoleLock = new object();
        private static void WriteLine(ConsoleColor color, string format,
            params object[] args)
        {
            lock (consoleLock)
            {
                var prev = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(format, args);
                Console.ForegroundColor = prev;
            }
        }
    }
}
