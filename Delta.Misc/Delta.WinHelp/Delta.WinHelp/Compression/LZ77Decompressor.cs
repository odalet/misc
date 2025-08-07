using System;
using System.IO;

namespace Delta.Compression
{
    // Grabbed from https://gist.github.com/Prof9/872e67a08e17081ca00e
    internal class LZ77Decompressor
    {
        /// <summary>
        /// Decompresses LZ77-compressed data from the given input stream.
        /// </summary>
        /// <param name="input">The input stream to read from.</param>
        /// <returns>The decompressed data.</returns>
        public static MemoryStream Decompress(Stream input)
        {
            var reader = new BinaryReader(input);

            // Check LZ77 type.
            if (reader.ReadByte() != 0x10)
                throw new ArgumentException("Input stream does not contain LZ77-compressed data.", "input");

            // Read the size.
            var size = reader.ReadUInt16() | (reader.ReadByte() << 16);

            // Create output stream.
            var output = new MemoryStream(size);

            // Begin decompression.
            while (output.Length < size)
            {
                // Load flags for the next 8 blocks.
                var flags = reader.ReadByte();

                // Process the next 8 blocks.
                for (int i = 0; i < 8; i++)
                {
                    // Check if the block is compressed.
                    if ((flags & (0x80 >> i)) == 0)
                    {
                        // Uncompressed block; copy single byte.
                        output.WriteByte(reader.ReadByte());
                    }
                    else
                    {
                        // Compressed block; read block.
                        var block = reader.ReadUInt16();
                        // Get byte count.
                        var count = ((block >> 4) & 0xF) + 3;
                        // Get displacement.
                        var displacement = ((block & 0xF) << 8) | ((block >> 8) & 0xFF);

                        // Save current position and copying position.
                        var outputPosition = output.Position;
                        var copyPosition = output.Position - displacement - 1;

                        // Copy all bytes.
                        for (var j = 0; j < count; j++)
                        {
                            // Read byte to be copied.
                            output.Position = copyPosition++;
                            var toWrite = (byte)output.ReadByte();

                            // Write byte to be copied.
                            output.Position = outputPosition++;
                            output.WriteByte(toWrite);
                        }
                    }

                    // If all data has been decompressed, stop.
                    if (output.Length >= size)
                        break;
                }
            }

            output.Position = 0;
            return output;
        }
    }
}
