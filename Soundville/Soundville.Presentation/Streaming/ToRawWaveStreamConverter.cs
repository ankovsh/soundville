using System;
using System.IO;
using System.Linq;
using NAudio.Wave;
using Soundville.Domain.Models;
using Soundville.Domain.Services.Interfaces;
using Soundville.Infrastructure.Constants;
using Soundville.Infrastructure.WindsorCastle;

namespace Soundville.Presentation.Streaming
{
    public class ToRawWaveStreamConverter : WaveStream
    {
        private readonly IStationDomainService _stationDomainService;
        private readonly IStationSongDomainService _stationSongDomainService;

        private MediaFoundationReader mediaFoundationReader;
        private readonly int streamId;
        private int position;
        private WaveFormatConversionStream waveFormatConversionStream;
        private WaveFormat _newFormat;
        private Mp3Stream mp3Stream;

        public ToRawWaveStreamConverter(Mp3Stream mp3Stream, int streamId, string songDirPath, WaveFormat newFormat)
        {
            _stationDomainService = IoC.ContainerInstance.Resolve<IStationDomainService>();
            _stationSongDomainService = IoC.ContainerInstance.Resolve<IStationSongDomainService>();
            this.streamId = streamId;
            position = 1;
            var stationSong = _stationSongDomainService.GetByPosition(streamId, position);
            string songPath = Path.Combine(songDirPath, stationSong.FileName);
            mediaFoundationReader = new MediaFoundationReader(songPath);
            _newFormat = newFormat;
            waveFormatConversionStream = new WaveFormatConversionStream(_newFormat, mediaFoundationReader);
            this.mp3Stream = mp3Stream;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            lock (buffer)
            {
                var readCount = waveFormatConversionStream.Read(buffer, offset, count);

                if ((waveFormatConversionStream.Position == waveFormatConversionStream.Length || readCount == 0))
                {
                    var stationSong = _stationSongDomainService.GetByPosition(streamId, ++position);
                    if (stationSong == null)
                    {
                        return 0;
                    }

                    mp3Stream.ResetLastSongByteCount();
                }

                return readCount;
            }
        }

        public override WaveFormat WaveFormat
        {
            get { return waveFormatConversionStream.WaveFormat; }
        }

        public override long Length
        {
            get { return long.MaxValue; }
        }

        public override long Position { get; set; }

        protected override void Dispose(bool disposing)
        {
            waveFormatConversionStream.Close();
            base.Dispose(disposing);
        }
    }
}
