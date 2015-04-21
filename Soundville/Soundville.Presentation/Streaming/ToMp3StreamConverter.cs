using System;
using System.IO;
using NAudio.Lame;
using NAudio.Wave;

namespace Soundville.Presentation.Streaming
{
    public class ToMp3StreamConverter : WaveStream
    {
        private readonly LameMP3FileWriter _lameMp3FileWriter;
        private readonly MemoryStream _outMemoryStream;
        private readonly WaveStream _inputWaveStream;
        private readonly int _bitRate;
        private readonly WaveFormat _outputWaveFormat;

        public ToMp3StreamConverter(WaveStream inputStream, int bitRate)
        {
            _bitRate = bitRate;
            _inputWaveStream = inputStream;
            _outMemoryStream = new MemoryStream();
            _lameMp3FileWriter = new LameMP3FileWriter(_outMemoryStream, inputStream.WaveFormat, _bitRate);
            _outputWaveFormat = new Mp3WaveFormat(inputStream.WaveFormat.SampleRate, inputStream.WaveFormat.Channels, _bitRate * 13 / 4, _bitRate * 1000);
        }

        public override WaveFormat WaveFormat
        {
            get
            {
                return _outputWaveFormat;
            }
        }

        public override long Length
        {
            get { return long.MaxValue; }
        }

        public override long Position { get; set; }

        protected override void Dispose(bool disposing)
        {
            _lameMp3FileWriter.Close();
            base.Dispose(disposing);
        }

        public override int Read(byte[] outBuffer, int offset, int count)
        {
            Clear(_outMemoryStream);
            var time = ((decimal)(count * 3)) / (_bitRate * 1000);
            var buffer = new byte[(int)(_inputWaveStream.WaveFormat.AverageBytesPerSecond * time)];
            /*            var buffer = new byte[count];*/
            var readCount = _inputWaveStream.Read(buffer, offset, buffer.Length);
            _lameMp3FileWriter.Write(buffer, offset, readCount);
            _outMemoryStream.Position = 0;
            _outMemoryStream.Read(outBuffer, offset, (int)_outMemoryStream.Length);
            return (int)_outMemoryStream.Length;
        }

        public static void Clear(MemoryStream source)
        {
            byte[] buffer = source.GetBuffer();
            Array.Clear(buffer, 0, buffer.Length);
            source.Position = 0;
            source.SetLength(0);
        }
    }
}
