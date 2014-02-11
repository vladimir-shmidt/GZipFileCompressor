using System;
using System.IO;
using System.IO.Compression;

namespace GZipFileCompressor
{
    class Program
    {
        private const int BlockLenght = 16384;

        static int Main(string[] args)
        {
            CompressionMode mode;
            if (ArgumentsHasNotRequiredValues(args, out mode))
            {
                Console.WriteLine("Specify command line arguments like: GZipFileCompressor.exe [compress|decompress] [fileName|archiveName] [archiveName|fileName]");
                return (int)Result.Error;
            }

            var result = (int)Operation(args[1], args[2], mode);
            Console.ReadLine();
            return result;
        }

        private static bool ArgumentsHasNotRequiredValues(string[] args, out CompressionMode action)
        {
            action = default(CompressionMode);
            return args == null || args.Length != 3 || !Enum.TryParse(args[0], true, out action) || string.IsNullOrEmpty(args[1]) || string.IsNullOrEmpty(args[2]);
        }

        private static Result Operation(string fileName, string archiveName, CompressionMode mode)
        {
            GZipStream gzipStream = null;
            Stream file = null;
            try
            {
                IOperation operation = OperationsFactory.Build(mode, BlockLenght);
                operation.Act(archiveName, fileName, out file, out gzipStream);
            }
            catch (Exception ex)
            {
                FileReadExceptionHandler.Handle(ex);
                return Result.Error;
            }
            finally
            {
                if (gzipStream != null) gzipStream.Dispose();
                if (file != null) file.Dispose();
            }
            return Result.Success;
        }
    }
}
