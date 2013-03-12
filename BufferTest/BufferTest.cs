namespace ConcurrentLibrary.BufferTest
{
    using ConcurrentLibrary;
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;
    using System.Diagnostics;

    abstract class BufferTest
    {
        protected IBuffer<string> buffer1, buffer2;
        private FileStream file;

        protected abstract void InitializeBuffers();

        public void BeginTest(string logPath)
        {
            file = File.OpenWrite(logPath);

            InitializeBuffers();
            Debug.Assert(buffer1 != null && buffer2 != null);

            new Thread(ListenerThread) { Name = "Listener" }.Start();
            new Thread(ProcessingThread) { Name = "Processing 1" }.Start();
            new Thread(ProcessingThread) { Name = "Processing 2" }.Start();
            new Thread(LogThread) { Name = "Log" }.Start();
        }

        private void ListenerThread()
        {
            while (true)
            {
                string line = Console.ReadLine();
                buffer1.Put(line);
            }
        }

        private void ProcessingThread()
        {
            SHA512 sha = SHA512.Create();

            while (true)
            {
                string dataString = buffer1.Take();

                DateTime timestamp = DateTime.Now;

                byte[] dataBytes = Encoding.UTF8.GetBytes(dataString);
                byte[] hashBytes = sha.ComputeHash(dataBytes);
                string hashString = Convert.ToBase64String(hashBytes);
                string logString = string.Format("{0}: {1} - {2}\n",
                    timestamp, dataString, hashString);
                byte[] logBytes = Encoding.UTF8.GetBytes(logString);
                lock (file)
                {
                    file.Write(logBytes, 0, logBytes.Length);
                    file.Flush();
                }
                buffer2.Put(dataString);
            }
        }

        private void LogThread()
        {
            while (true)
            {
                string log = buffer2.Take();
                Console.WriteLine("Logged {0}", log);
            }
        }
    }
}
