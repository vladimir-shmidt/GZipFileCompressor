using System.IO;
using System.IO.Compression;

namespace GZipFileCompressor
{
    public interface IOperation
    {
        void Act(string archiveName, string fileName, out Stream file, out GZipStream gzipStream);
    }
}