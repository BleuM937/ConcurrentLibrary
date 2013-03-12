namespace ConcurrentLibrary.BufferTest
{
    using ConcurrentLibrary;

    sealed class BalkingBufferTest : BufferTest
    {
        protected override void InitializeBuffers()
        {
            buffer1 = new BalkingBuffer<string>();
            buffer2 = new BalkingBuffer<string>();
        }

        static void Main(string[] args)
        {
            var test = new BalkingBufferTest();

            string logPath = "log.txt";
            if (args.Length > 0) logPath = args[0];

            test.BeginTest(logPath);
        }
    }
}
