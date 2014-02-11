using System.IO.Compression;

namespace GZipFileCompressor
{
    public class GZipStreamInitiializer
    {
        public static GZipStream Initializer(CompressionMode mode, string fileName)
        {
            return InitStrategyFactory.BuildStrategy(mode).InitStream(fileName);
        }
    }
}