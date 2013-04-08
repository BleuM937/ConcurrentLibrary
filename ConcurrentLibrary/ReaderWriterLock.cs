using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConcurrentLibrary
{
    using System;

    public class ReaderWriterLock
    {
        private readonly Semaphore
            readTurnstile = new Semaphore(1),
            writeTurnstile = new Semaphore(1),
            writePerm = new Semaphore(1);
        private readonly LightSwitch readPerm;

        public ReaderWriterLock()
        {
            readPerm = new LightSwitch(writePerm);
        }

        public void AcquireWrite()
        {
            writeTurnstile.Acquire();
            readTurnstile.Acquire();
            writePerm.Acquire();
        }

        public void ReleaseWrite()
        {
            writePerm.Release();
            readTurnstile.Release();
            writeTurnstile.Release();
        }

        public void AcquireRead()
        {
            readTurnstile.Acquire();
            readTurnstile.Release();
            readPerm.Acquire();

        }
        public void ReleaseRead()
        {
            readPerm.Release();
        }
    }
}
