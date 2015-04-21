using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Soundville.Presentation.Streaming
{
    public class MyMemoryStream : Stream
    {
        public override sealed long Position { get; set; }

        public MyMemoryStream(List<byte> buffer)
        {
            _buffer = buffer;
            Position = 0;
        }

        private readonly List<byte> _buffer;

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush() { }

        public override long Length
        {
            get { return _buffer.Count; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var byteCount = _buffer.Count - Position;
            if (byteCount > count)
                byteCount = count;
            if (byteCount <= 0)
                return 0;
            Array.Copy(_buffer.Skip(offset).Take(count).ToArray(), 0, buffer, offset, count);
            Position += byteCount;
            return count;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long newPos = 0;

            switch (origin)
            {
                case SeekOrigin.Begin:
                    newPos = offset;
                    break;
                case SeekOrigin.Current:
                    newPos = Position + offset;
                    break;
                case SeekOrigin.End:
                    newPos = Length - offset;
                    break;
            }

            Position = Math.Max(0, Math.Min(newPos, Length));
            return newPos;
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _buffer.AddRange(buffer.Skip(offset).Take(count));
        }
    }
}
