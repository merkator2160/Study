using System;
using System.Threading;

namespace CodeForces.Units.Threads
{
    public class ThreadObject : IDisposable
    {
        private Boolean _disposed;


        public ThreadObject()
        {
            ThreadPool.QueueUserWorkItem(TestingThread);
        }


        // THREADS ////////////////////////////////////////////////////////////////////////////////
        private void TestingThread(Object state)
        {
            while (!_disposed)
            {
                Console.WriteLine($"{nameof(TestingThread)} still alive!");
                Thread.Sleep(500);
            }
        }


        // IDisposable ////////////////////////////////////////////////////////////////////////////
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
            }
        }
    }
}