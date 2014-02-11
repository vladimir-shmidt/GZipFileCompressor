using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;

namespace GZipFileCompressor
{
    public sealed class ParallelFileWriter : Stream
    {
        private readonly ConcurrentQueue<byte[]> _queue = new ConcurrentQueue<byte[]>();
        private readonly FileStream _stream;
        private readonly QueuedThreadPool _threadPool; 

        public ParallelFileWriter(string filename)
        {
            _stream = new FileStream(filename, FileMode.Create);
            _threadPool = new QueuedThreadPool(1);
        }

        public void Write(byte[] data)
        {
            _queue.Enqueue(data);
            if (_queue.IsEmpty) return;
            _threadPool.PoolJob((Action)WriterTask);
        }

        protected override void Dispose(bool disposing)
        {
            _threadPool.WaitAll();
            _threadPool.Dispose();
            if (_stream != null){_stream.Flush();_stream.Close();_stream.Dispose();}
            base.Dispose(disposing);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            Write(buffer.Skip(offset).Take(count).ToArray());
        }

        private void WriterTask()
        {
            while (!_queue.IsEmpty)
            {
                byte[] buffer;
                _queue.TryDequeue(out buffer);
                _stream.Write(buffer, 0, buffer.Length);
            }
        }

        #region Not Supported Implementation of Stream

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead
        {
            get { throw new NotImplementedException(); }
        }

        public override bool CanSeek
        {
            get { throw new NotImplementedException(); }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public override long Position { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        #endregion
    }
}