using System;
using System.Collections.Concurrent;
using System.Threading;

namespace GZipFileCompressor
{
    public class QueuedThreadPool
    {
        private const int PreventCpuBurnOut = 50;
        private readonly int _workerCount;
        private readonly object _suncRoot = new object();
        readonly ConcurrentDictionary<int, Worker> _workers = new ConcurrentDictionary<int, Worker>();
        readonly ConcurrentQueue<JobData> _pooledWork = new ConcurrentQueue<JobData>();
        private readonly AutoResetEvent _resetEvent = new AutoResetEvent(false);

        public QueuedThreadPool(int workerCount)
        {
            _workerCount = workerCount;
        }

        public void WaitAll()
        {
            var allPooledDone = new AutoResetEvent(false);
            PoolJob((Action<AutoResetEvent>)(re => re.Set()), allPooledDone);   
            allPooledDone.WaitOne();
        }

        public void PoolJob(JobData jobData)
        {
            lock (_suncRoot)
            {
                if (_workers.Count < _workerCount)
                {
                    var worker = new Worker((Action)Work);
                    _workers.TryAdd(worker.GetHashCode(), worker);
                }
                _pooledWork.Enqueue(jobData);
                Monitor.PulseAll(_suncRoot);
            }
        }

        public void PoolJob(Delegate action, params object[] args)
        {
            PoolJob(new JobData { Action = action, Arguments = args });
        }

        void Work()
        {
            while (!_resetEvent.WaitOne(PreventCpuBurnOut, false))
            {
                JobData job;
                lock (_suncRoot)
                {
                    while (_pooledWork.IsEmpty) Monitor.Wait(_suncRoot);
                    _pooledWork.TryDequeue(out job);
                }

                Delegate action = job.Action;
                object[] args = job.Arguments;

                try { action.DynamicInvoke(args); }
                catch (Exception) { }
            }
        }


        public void Dispose()
        {
            _resetEvent.Set();
        }
    }
}