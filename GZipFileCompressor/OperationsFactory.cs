using System;
using System.IO.Compression;

namespace GZipFileCompressor
{
    public class OperationsFactory
    {
        public static IOperation Build(CompressionMode mode, int blockLenght)
        {
            switch (mode)
            {
                case CompressionMode.Compress: return new Compress(blockLenght);
                case CompressionMode.Decompress: return new Decompress(blockLenght);
                default: throw new ArgumentOutOfRangeException("mode");
            }
        }
    }
}