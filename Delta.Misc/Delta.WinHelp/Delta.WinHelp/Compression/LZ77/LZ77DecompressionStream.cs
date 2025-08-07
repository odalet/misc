using System;
using System.IO;

namespace Delta.Compression.LZ77
{
    // TODO: Begin/End Read
    internal class LZ77DecompressionStream : Stream
    {
        private const int defaultBufferSize = 4096;

        private readonly Stream innerStream;
        private byte[] innerBuffer = new byte[defaultBufferSize];
        private bool disposed = false;

        public LZ77DecompressionStream(Stream inputStream)
        {
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");
            if (!inputStream.CanWrite)
                throw new ArgumentException("Specified input stream must be readable", "inputStream");

            innerStream = inputStream;
        }

        public override bool CanRead
        {
            get { return !disposed; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        public override long Position
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (disposed) 
                throw new ObjectDisposedException("innerStream");
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");
            if (buffer.Length - offset < count)
                throw new ArgumentException("Offset and count overflow the specified buffer");

            return Decompress(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override void Flush()
        {
            if (disposed) 
                throw new ObjectDisposedException("innerStream");
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.IO.Stream" /> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!disposed && disposing)
                    innerStream.Close();
                disposed = true;
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private int Decompress(byte[] output, int offset, int count)
        {
            //while (true)
            //{

            //}

            return innerStream.Read(output, offset, count);

            //var mbuffer = new MemoryStream(output, offset, count);
            //var writer = new BinaryWriter(mbuffer);
            //var reader = new BinaryReader(innerStream);

            //reader.ReadBytes()
            //throw new NotImplementedException("TODO");
        }
    }
}
