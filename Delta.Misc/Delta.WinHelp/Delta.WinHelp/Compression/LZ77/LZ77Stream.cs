using System;
using System.IO;
using System.IO.Compression;

namespace Delta.Compression.LZ77
{
    public class LZ77Stream : Stream
    {
        private readonly Stream innerStream;
        private bool disposed = false;

        public LZ77Stream(Stream stream, CompressionMode mode)
        {
            if (stream == null) throw new ArgumentNullException("stream");

            innerStream = mode == CompressionMode.Compress ?
                (Stream)new LZ77CompressionStream(stream) :
                (Stream)new LZ77DecompressionStream(stream);
        }

        public override bool CanRead
        {
            get { return !disposed && innerStream.CanRead; }
        }

        public override bool CanWrite
        {
            get { return !disposed && innerStream.CanWrite; }
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

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            if (disposed) throw new ObjectDisposedException("innerStream");
            return innerStream.BeginRead(buffer, offset, count, callback, state);
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            if (disposed) throw new ObjectDisposedException("innerStream");
            return innerStream.BeginWrite(buffer, offset, count, callback, state);
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            if (disposed) throw new ObjectDisposedException("innerStream");
            return innerStream.EndRead(asyncResult);
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            if (disposed) throw new ObjectDisposedException("innerStream");
            innerStream.EndWrite(asyncResult);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (disposed) throw new ObjectDisposedException("innerStream");
            return innerStream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (disposed) throw new ObjectDisposedException("innerStream");
            innerStream.Write(buffer, offset, count);
        }

        public override void Flush()
        {
            if (disposed) throw new ObjectDisposedException("innerStream");
            innerStream.Flush();
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
    }
}
