using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConcurrentLibrary.BufferTest
{
    class ChannelTest : BufferTest
    {
        protected override void InitializeBuffers()
        {
            buffer1 = new Channel<string>();
            buffer2 = new Channel<string>();
        }

        static void Main(string[] args)
        {
            ChannelTest test = new ChannelTest();

            string logPath = "log.txt";
            if (args.Length > 0) logPath = args[0];

            test.BeginTest(logPath);
        }
    }
}
