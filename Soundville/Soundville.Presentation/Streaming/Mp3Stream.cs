using System;
using System.Threading;
using System.Timers;
using NAudio.Wave;

namespace Soundville.Presentation.Streaming
{
    public class Mp3Stream : IWaveProvider
    {
        public event EventHandler<byte[]> Listener;
        public event EventHandler ClearBuferEvent;

        public event EventHandler<StreamingPlaybackState> SetStatus;

        private readonly System.Timers.Timer _timer;
        private BufferedWaveProvider _bufferedWaveProvider;
        private volatile StreamingPlaybackState _playbackState;
        private WaveStream _mp3Stream;
        private WaveFormat _newFormat;
        private int _interval = 1000;
        private int _bitRate;
        private readonly ToRawWaveStreamConverter _waveStreamConverter;
        private readonly int _stationId;
        private long _indexCount;
        private readonly object _lockStream = new object();
        private int _lastSongByteCount;

        public void ResetLastSongByteCount()
        {
            _lastSongByteCount = 0;
        }

        public void TouchUpBuffer()
        {
            var buffer = new byte[_bufferedWaveProvider.BufferedBytes - _lastSongByteCount];
            lock (_bufferedWaveProvider)
            {
                var count = _bufferedWaveProvider.Read(buffer, 0, buffer.Length);
                _bufferedWaveProvider.ClearBuffer();
                _bufferedWaveProvider.AddSamples(buffer, 0, count);
            }
        }

        public Mp3Stream(int stationId, string songDirPath, int bitRate)
        {
            _playbackState = StreamingPlaybackState.Stopped;
            _stationId = stationId;
            _bitRate = bitRate;
            _newFormat = new WaveFormat(44100, 16, 2);
            _waveStreamConverter = new ToRawWaveStreamConverter(this, _stationId, songDirPath, _newFormat);
            _mp3Stream = new ToMp3StreamConverter(_waveStreamConverter, _bitRate);
            _timer = new System.Timers.Timer(_interval);
            _timer.Elapsed += OnTimedEvent;
            _lastSongByteCount = 0;
        }

        public void SetInterval(int interval)
        {
            _interval = interval;
        }

        protected virtual void OnClearBuferEvent()
        {
            EventHandler handler = ClearBuferEvent;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void OnListener(byte[] e)
        {
            EventHandler<byte[]> handler = Listener;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnSetStatus()
        {
            EventHandler<StreamingPlaybackState> handler = SetStatus;
            if (handler != null)
            {
                handler(this, _playbackState);
            }
        }

        private void StartBuffered(object state)
        {
            var buffer = new byte[16384 * 8]; // needs to be big enough to hold a decompressed frame
            _indexCount = 0;
            if (_bufferedWaveProvider == null)
            {
                // don't think these details matter too much - just help ACM select the right codec
                // however, the buffered provider doesn't know what sample rate it is working at
                // until we have a frame
                _bufferedWaveProvider = new BufferedWaveProvider(_mp3Stream.WaveFormat)
                {
                    BufferDuration = TimeSpan.FromSeconds(60)
                };
            }

            do
            {
                if (IsBufferNearlyFull || _playbackState == StreamingPlaybackState.Paused)
                {
                    Thread.Sleep(500);
                }
                else
                {
                    int count = _mp3Stream.Read(buffer, 0, buffer.Length);
                    _lastSongByteCount += count;
                    if (count == 0)
                    {
                        Thread.Sleep(900);
                        _indexCount++;
                        if (_indexCount > 10)
                        {
                            _playbackState = StreamingPlaybackState.Stopped;
                            _mp3Stream.Read(buffer, 0, buffer.Length);
                        }
                    }
                    else
                    {
                        _indexCount = 0;
                        _bufferedWaveProvider.AddSamples(buffer, 0, count);
                    }
                }

            } while (_playbackState != StreamingPlaybackState.Stopped);
            //Debug.WriteLine("Exiting");
            // was doing this in a finally block, but for some reason
            // we are hanging on response stream .Dispose so never get there
        }

        private bool IsBufferNearlyFull
        {
            get
            {
                return _bufferedWaveProvider != null &&
                       _bufferedWaveProvider.BufferLength - _bufferedWaveProvider.BufferedBytes
                       < _bufferedWaveProvider.BufferLength / 4;
            }
        }

        public void PlayStream()
        {
            if (_playbackState != StreamingPlaybackState.Stopped)
            {
                return;
            }

            if (_bufferedWaveProvider == null)
            {
                _bufferedWaveProvider = new BufferedWaveProvider(_mp3Stream.WaveFormat)
                {
                    BufferDuration = TimeSpan.FromSeconds(60)
                };
            }

            ClearBuffer();
            _playbackState = StreamingPlaybackState.Playing;
            ThreadPool.QueueUserWorkItem(StartBuffered, string.Empty);
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            var buffer = new byte[16384]; // magic number
            var count = _bufferedWaveProvider.Read(buffer, 0, buffer.Length);
            if (count > 0)
            {
                OnListener(buffer);
            }
        }

        public void Stop()
        {
            _playbackState = StreamingPlaybackState.Stopped;
            _timer.Enabled = false;
            OnSetStatus();
        }

        public void Play()
        {
            lock (_lockStream)
            {
                PlayStream();
                _playbackState = StreamingPlaybackState.Playing;
                OnSetStatus();
                _timer.Enabled = true;
            }
        }

        public void Pause()
        {
            _playbackState = StreamingPlaybackState.Paused;
            _timer.Enabled = false;
            OnSetStatus();
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            return _bufferedWaveProvider.Read(buffer, offset, count);
        }

        public int BuferSize { get { return _mp3Stream.WaveFormat.AverageBytesPerSecond * _interval / 1000; } }

        public WaveFormat WaveFormat
        {
            get { return _mp3Stream.WaveFormat; }
        }

        public StreamingPlaybackState Status
        {
            get { return _playbackState; }
        }

        public void PlaySong()
        {
            Stop();
            Thread.Sleep(1000);
            ClearBuffer();
            Play();
        }

        private void ClearBuffer()
        {
            if (_bufferedWaveProvider != null)
            {
                _bufferedWaveProvider.ClearBuffer();
            }

            OnClearBuferEvent();
        }
    }
}
