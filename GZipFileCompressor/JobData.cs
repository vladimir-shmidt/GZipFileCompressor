using System;

namespace GZipFileCompressor
{
    public class JobData
    {
        public Delegate Action { get; set; }
        public object[] Arguments { get; set; }
    }
}