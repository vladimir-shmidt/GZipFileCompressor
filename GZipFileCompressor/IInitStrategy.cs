using System.IO.Compression;

namespace GZipFileCompressor
{
    public interface IInitStrategy
    {
        GZipStream InitStream(string fileName);
    }
}