using System;
using System.IO;

namespace GZipFileCompressor
{
    class FileReadExceptionHandler
    {
        public static void Handle(Exception exception)
        {
            if (exception is PathTooLongException) Handle((PathTooLongException)exception);
            else if (exception is DirectoryNotFoundException) Handle((DirectoryNotFoundException)exception);
            else if (exception is UnauthorizedAccessException) Handle((UnauthorizedAccessException)exception);
            else if (exception is FileNotFoundException) Handle((FileNotFoundException)exception);
            else if (exception is NotSupportedException) Handle((NotSupportedException)exception);
        }

        public static void Handle(PathTooLongException exception)
        {
            ConsoleLogExceptionMessage(exception);
        }

        public static void Handle(DirectoryNotFoundException exception)
        {
            ConsoleLogExceptionMessage(exception);
        }

        public static void Handle(UnauthorizedAccessException exception)
        {
            ConsoleLogExceptionMessage(exception);
        }

        public static void Handle(FileNotFoundException exception)
        {
            ConsoleLogExceptionMessage(exception);
        }

        public static void Handle(NotSupportedException exception)
        {
            ConsoleLogExceptionMessage(exception);
        }

        private static void ConsoleLogExceptionMessage(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}