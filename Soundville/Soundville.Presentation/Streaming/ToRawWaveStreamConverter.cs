using System.IO;
using NAudio.Wave;
using Soundville.Domain.Models;
using Soundville.Domain.Services;
using Soundville.Domain.Services.Interfaces;
using Soundville.Infrastructure.Constants;

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
        private string _songDirPath;

        public ToRawWaveStreamConverter(Mp3Stream mp3Stream, int streamId, string songDirPath, WaveFormat newFormat)
        {
            _stationDomainService = new StationDomainService();
            _stationSongDomainService = new StationSongDomainService();
            this.streamId = streamId;
            position = 1;
            var stationSong = _stationSongDomainService.GetByPosition(streamId, position);
            _songDirPath = songDirPath;
            string songPath = GetSongPath(stationSong.FileName);
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
                        if (StreamingPlaybackState.Stopped == mp3Stream.Status)
                        {
                            var station = _stationDomainService.GetStationById(streamId);
                            station.Status = StationStatus.Finished;
                            _stationDomainService.Save(station);
                        }
                       
                        return 0;
                    }

                    RestartReading(stationSong);
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

        private void RestartReading(StationSong stationSong)
        {
            if (waveFormatConversionStream != null)
            {
                waveFormatConversionStream.Close();
            }

            mediaFoundationReader = new MediaFoundationReader(GetSongPath(stationSong.FileName));
            waveFormatConversionStream = new WaveFormatConversionStream(_newFormat, mediaFoundationReader);
        }

        private string GetSongPath(string fileName)
        {
            return Path.Combine(_songDirPath, fileName);
        }
    }
}
