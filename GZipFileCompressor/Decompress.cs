using System.IO;
using System.IO.Compression;

namespace GZipFileCompressor
{
    public class Decompress : IOperation
    {
        private readonly int _blockLenght;

        public Decompress(int blockLenght)
        {
            _blockLenght = blockLenght;
        }

        public void Act(string archiveName, string fileName, out Stream file, out GZipStream gzipStream)
        {
            file = new ParallelFileWriter(fileName);
            gzipStream = GZipStreamInitiializer.Initializer(CompressionMode.Decompress, archiveName);
            int bytesRead;
            var buffer = new byte[_blockLenght];
            do
            {
                bytesRead = gzipStream.Read(buffer, 0, _blockLenght);
                file.Write(buffer, 0, bytesRead);
            } while (bytesRead != 0);
        }
    }
}