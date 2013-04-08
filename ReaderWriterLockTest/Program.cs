namespace ConcurrentLibrary.ReaderWriterLockTest
{
    using System;
    using Thread = System.Threading.Thread;

    class Program
    {
        static void Main(string[] args)
        {
            ReaderWriterLock rwLock = new ReaderWriterLock();

            for (int i = 0; i < 4; i++)
            {
                new Thread(() =>
                {
                    while (true)
                    {
                        var r = new Random();
                        Console.WriteLine("{0} acquiring write", Thread.CurrentThread.Name);
                        rwLock.AcquireWrite();
                        Console.WriteLine("{0} acquired write", Thread.CurrentThread.Name);
                        Thread.Sleep(r.Next(3000, 5000));
                        Console.WriteLine("{0} releasing write", Thread.CurrentThread.Name);
                        rwLock.ReleaseWrite();
                    }
                }) { Name = "Writer " + i }.Start();
            }

            for (int i = 0; i < 12; i++)
            {
                new Thread(() =>
                    {
                        while (true)
                        {
                            var r = new Random();
                            Console.WriteLine("{0} acquiring read", Thread.CurrentThread.Name);
                            rwLock.AcquireRead();
                            Console.WriteLine("{0} acquired read", Thread.CurrentThread.Name);
                            Thread.Sleep(r.Next(250, 500));
                            Console.WriteLine("{0} releasing read", Thread.CurrentThread.Name);
                            rwLock.ReleaseRead();
                        }
                    }) { Name = "Reader " + i }.Start();
            }
        }
    }
}
