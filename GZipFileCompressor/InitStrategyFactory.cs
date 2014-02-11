using System;
using System.IO.Compression;

namespace GZipFileCompressor
{
    public class InitStrategyFactory
    {
        public static IInitStrategy BuildStrategy(CompressionMode mode)
        {
            switch (mode)
            {
                case CompressionMode.Compress: return new CompressInit();
                case CompressionMode.Decompress: return new DecompressInit();
                default: throw new ArgumentOutOfRangeException("mode");
            }
        }
    }
}