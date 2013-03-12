using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConcurrentLibrary
{
    public class Latch
    {
        private Semaphore ready = new Semaphore(0);

        public void Acquire()
        {
            ready.Acquire();
            ready.Release();
        }

        public void Release()
        {
            ready.Release();
        }
    }
}
