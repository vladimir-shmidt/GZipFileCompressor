using System.IO;
using System.IO.Compression;

namespace GZipFileCompressor
{
    public class CompressInit : IInitStrategy
    {
        public GZipStream InitStream(string fileName)
        {
            Stream file = new ParallelFileWriter(fileName);
            return new GZipStream(file, CompressionMode.Compress);
        }
    }
}