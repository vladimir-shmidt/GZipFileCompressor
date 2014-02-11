using System.IO;
using System.IO.Compression;

namespace GZipFileCompressor
{
    public class Compress : IOperation
    {
        private readonly int _blockLenght;

        public Compress(int blockLenght)
        {
            _blockLenght = blockLenght;
        }

        public void Act(string archiveName, string fileName, out Stream file, out GZipStream gzipStream)
        {
            file = File.OpenRead(fileName);
            gzipStream = GZipStreamInitiializer.Initializer(CompressionMode.Compress, archiveName);
            int bytesRead;
            var buffer = new byte[_blockLenght];
            do
            {
                bytesRead = file.Read(buffer, 0, _blockLenght);
                gzipStream.Write(buffer, 0, bytesRead);
            } while (bytesRead != 0);
        }
    }
}