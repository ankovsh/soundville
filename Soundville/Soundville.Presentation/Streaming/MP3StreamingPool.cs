using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Soundville.Presentation.Streaming
{
    public class Mp3StreamingPool
    {
        private static volatile Mp3StreamingPool _instance;
        private static readonly object SyncRoot = new object();

        private Dictionary<int, Mp3Stream> Mp3Streams { get; set; }

        private Mp3StreamingPool()
        {
            Mp3Streams = new Dictionary<int, Mp3Stream>();
        }

        public static Mp3StreamingPool Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null) _instance = new Mp3StreamingPool();
                    }
                }

                return _instance;
            }
        }

        public void StartMp3Streaming(int streamId, string songDirPath, int bitRate)
        {
            lock (Mp3Streams)
            {
                if (!Mp3Streams.ContainsKey(streamId))
                {
                    Mp3Streams.Add(streamId, new Mp3Stream(streamId, songDirPath, bitRate));
                }
            }
        }

        public bool IsStreamExist(int streamId)
        {
            return Mp3Streams.ContainsKey(streamId);
        }

        public StreamingPlaybackState GetState(int streamId)
        {
            var mp3Stream = GetStream(streamId);
            return mp3Stream.Status;
        }

        public void PlaySong(int streamId)
        {
            Mp3Stream mp3Stream;
            if (!Mp3Streams.TryGetValue(streamId, out mp3Stream))
            {
                throw new Exception("Stream is not started.");
            }

            mp3Stream.PlaySong();
        }

        public Mp3Stream GetStream(int streamId)
        {
            Mp3Stream mp3Stream;
            if (!Mp3Streams.TryGetValue(streamId, out mp3Stream))
            {
                throw new Exception("Mp3Stream was not found.");
            }

            return mp3Stream;
        }

        public void PauseStream(int streamId)
        {
            var stream = GetStream(streamId);
            stream.Pause();
        }

        public void ResumeStream(int streamId)
        {
            var stream = GetStream(streamId);
            stream.Play();
        }

        public void CheckAddBinPath()
        {
            // find path to 'bin' folder
            var binPath = Path.Combine(new[] { AppDomain.CurrentDomain.BaseDirectory, "bin" });
            // get current search path from environment
            var path = Environment.GetEnvironmentVariable("PATH") ?? "";

            // add 'bin' folder to search path if not already present
            if (path.Split(Path.PathSeparator).ToArray().Contains(binPath, StringComparer.CurrentCultureIgnoreCase))
            {
                return;
            }

            path = string.Join(Path.PathSeparator.ToString(CultureInfo.InvariantCulture), path, binPath);
            Environment.SetEnvironmentVariable("PATH", path);
        }
    }
}
