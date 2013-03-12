using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConcurrentLibrary.BufferTest
{
    class WaitBufferTest : BufferTest
    {
        protected override void InitializeBuffers()
        {
            buffer1 = new WaitBuffer<string>();
            buffer2 = new WaitBuffer<string>();
        }

        static void Main(string[] args)
        {
            var test = new WaitBufferTest();
            string logPath = "log.txt";

            if (args.Length > 0) logPath = args[0];

            test.BeginTest(logPath);
        }
    }
}
