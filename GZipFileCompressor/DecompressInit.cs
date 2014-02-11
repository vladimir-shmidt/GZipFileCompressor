using System.IO;
using System.IO.Compression;

namespace GZipFileCompressor
{
    public class DecompressInit : IInitStrategy
    {
        public GZipStream InitStream(string fileName)
        {
            Stream file = File.OpenRead(fileName);
            return new GZipStream(file, CompressionMode.Decompress);
        }
    }
}