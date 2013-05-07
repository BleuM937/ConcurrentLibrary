namespace RendezvousTest
{
    using System;
    using ConcurrentLibrary;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            Rendezvous<string> rendezvous = new Rendezvous<string>();

            var inputColors = new[] 
            {
                ConsoleColor.DarkRed,
                ConsoleColor.DarkGreen,
                ConsoleColor.DarkBlue,
                ConsoleColor.DarkYellow            
            };
            var outputColors = new[]
            {
                ConsoleColor.Red,
                ConsoleColor.Green,
                ConsoleColor.Blue,
                ConsoleColor.Yellow
            };

            for (int i = 0; i < 4; i++)
            {
                Thread.Sleep(1000);
                new Thread((param) =>
                {
                    int n = (int)param;
                    Random random = new Random();
                    for (int j = 0; true; j++)
                    {
                        Thread.Sleep(random.Next(2500, 4000));
                        string input = string.Concat(n, ":", j);
                        WriteLineWithColor(inputColors[n], "Thread {0} --> {1}", n, input);
                        string output = rendezvous.Exchange(input);
                        WriteLineWithColor(outputColors[n], "Thread {0} <-- {1}", n, output);
                    }
                }).Start(i);
            }
        }

        static readonly object consoleLock = new object();
        static void WriteLineWithColor(ConsoleColor color, string format, params object[] args)
        {
            lock (consoleLock)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(format, args);
            }
        }
    }
}
