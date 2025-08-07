using System;
using System.IO;
using System.Text;

namespace Delta.WinHelp.Parsing
{
    internal static class Helper
    {
        /// <summary>
        /// Decodes a Zero-terminated ASCII string from the supplied <paramref name="data"/> bytes.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>The string made of the supplied bytes until <c>'\0'</c> is found.</returns>
        public static string DecodeStringz(byte[] data)
        {
            var builder = new StringBuilder();

            for (var index = 0; index < data.Length; index++)
            {
                if (data[index] == 0)
                    break;

                var c = Convert.ToChar(data[index]);
                builder.Append(c);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Decodes a Zero-terminated ASCII string from the supplied binary stream <paramref name="reader"/>.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>The string made of the supplied bytes until <c>'\0'</c> is found.</returns>
        public static string DecodeStringz(BinaryReader reader)
        {
            // Note: there is probably a quicker way to retrieve stringz values: 
            // The string data is normally part of a structure whose header contains size information.
            // We should be able to know beforehand how long the string is supposed to be.
            // Have a look at http://stackoverflow.com/questions/144176/fastest-way-to-convert-a-possibly-null-terminated-ascii-byte-to-a-string
            // for a fast 'unsafe' implementation.

            var builder = new StringBuilder();
            var ok = true;
            while (ok)
            {
                var current = reader.ReadByte();
                if (current == 0)
                    break;

                var c = Convert.ToChar(current);
                builder.Append(c);
            }

            return builder.ToString();
        }

        // Similar to Unix Epoch, but starts in 1980, not 1970...
        private static readonly DateTime epoch1980 = new DateTime(1980, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime? DecodeDate(int seconds)
        {
            if (seconds <= 0) return null;
            return epoch1980.AddSeconds(seconds);
        }
    }
}
