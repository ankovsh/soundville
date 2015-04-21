using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;

namespace Soundville.Presentation.Streaming
{
    public class SoundStream : IDisposable
    {
        private readonly Queue<byte[]> _queue;
        private readonly Mp3Stream _mp3Stream;
        private StreamingPlaybackState _status;
        private int _sleepCount;

        public SoundStream(Mp3Stream mp3Stream)
        {
            _queue = new Queue<byte[]>();

            _mp3Stream = mp3Stream;
            _mp3Stream.Listener += FillQueue;
            _mp3Stream.ClearBuferEvent += ClearBufer;
            _mp3Stream.SetStatus += SetStatus;
            _status = mp3Stream.Status;
        }

        private void ClearBufer(object sender, EventArgs e)
        {
            lock (_queue)
            {
                _queue.Clear();
            }
        }

        private void SetStatus(object sender, StreamingPlaybackState e)
        {
            _status = e;
        }

        private void FillQueue(object sender, byte[] e)
        {
            _queue.Enqueue(e);
        }

        public async void WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
        {
            try
            {
                var buffer = new byte[65536];

                while (true)
                {
                    Byte[] bytes;
                    if (_sleepCount > 20) return;
                    lock (_queue)
                    {
                        if (_queue.Count == 0 || _status == StreamingPlaybackState.Paused)
                        {
                            _sleepCount++;
                            Thread.Sleep(500);
                            continue;
                        }

                        _sleepCount = 0;

                        bytes = _queue.Dequeue();
                    }
                    using (var stream = new MemoryStream(bytes))
                    {
                        var length = (int)stream.Length;
                        var bytesRead = 1;

                        while (length > 0 && bytesRead > 0)
                        {
                            bytesRead = stream.Read(buffer, 0, Math.Min(length, buffer.Length));
                            await outputStream.WriteAsync(buffer, 0, bytesRead);
                            length -= bytesRead;
                        }
                    }
                }
            }
            catch (HttpException ex)
            {
                // do nothing
            }
            finally
            {
                outputStream.Close();
            }
        }

        public void Dispose()
        {
            _mp3Stream.Listener -= FillQueue;
            _mp3Stream.SetStatus -= SetStatus;
        }
    }
}
