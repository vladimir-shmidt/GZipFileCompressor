using System;
using System.Threading;

namespace GZipFileCompressor
{
    public class Worker
    {
        private readonly Thread _thread;

        public Worker(Delegate action)
        {
            _thread = new Thread(() => action.DynamicInvoke()) { IsBackground = true };
            _thread.Start();
        }

        public void Join()
        {
            _thread.Join();
        }
    }
}