using System.IO;
using NAudio.Wave;
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
        private WaveFormatConversionStream waveFormatConversionStream;
        private WaveFormat _newFormat;
        private bool _isNext;
        private object lockObj;
        private Mp3Stream mp3Stream;

        public ToRawWaveStreamConverter(Mp3Stream mp3Stream, int streamId, string songDirPath, WaveFormat newFormat)
        {
            _stationDomainService = IoC.ContainerInstance.Resolve<IStationDomainService>();
            _stationSongDomainService = IoC.ContainerInstance.Resolve<IStationSongDomainService>();
            this.streamId = streamId;
            string songPath = Path.Combine(songDirPath, "1cfc6d55-711b-4fc8-8d8f-ef402ca64338.mp3");
            mediaFoundationReader = new MediaFoundationReader(songPath);
            _newFormat = newFormat;
            waveFormatConversionStream = new WaveFormatConversionStream(_newFormat, mediaFoundationReader);
            _isNext = false;
            this.mp3Stream = mp3Stream;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            lock (buffer)
            {
                int readCount = waveFormatConversionStream.Read(buffer, offset, count);

                /*if ((waveFormatConversionStream.Position == waveFormatConversionStream.Length || readCount == 0))
                {
                    var station = _stationDomainService.GetById(streamId);
                    if (station == null)
                    {
                        throw new Exception("Station was not found.");
                    }

                    if (station.StationSongs.Count < position + 1)
                    {
                        return 0;
                    }

                    StationSong currentStationSong;
                    do
                    {
                        position++;
                        currentStationSong = station.PartySongs.SingleOrDefault(x => x.CurrentPosition == position);
                        if (currentStationSong == null)
                        {
                            break;
                        }
                    } while (PartyHelper.IsPartySongMustBeSkipped(currentStationSong.Id, station.Id));
                    
                    if (station.PartySongs.Count == position)
                    {
                        station.CurrentSong = position;
                        _stationDomainService.Save(station);
                        return 0;
                    }

                    var partySong = station.PartySongs.SingleOrDefault(s => s.CurrentPosition == position);
                    if (partySong == null)
                    {
                        return 0;
                    }

                    mp3Stream.ResetLastSongByteCount();
                }*/

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
