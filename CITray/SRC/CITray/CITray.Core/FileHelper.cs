using System.IO;

namespace CITray
{
    internal class FileHelper
    {
        /// <summary>
        /// Creates the specified directory if needed.
        /// </summary>
        /// <param name="path">The path.</param>
        public static void CreateDirectoryIfNeeded(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
