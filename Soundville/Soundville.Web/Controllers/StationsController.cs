using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Soundville.Presentation.Streaming;

namespace Soundville.Web.Controllers
{
    //[RoutePrefix("stations")]
    public class StationsController : ApiController
    {
        //[Route("stream")]
        public HttpResponseMessage Get(int id)
        {
            var response = Request.CreateResponse();
            var mp3StreamingPool = Mp3StreamingPool.Instance;
            if (mp3StreamingPool.IsStreamExist(id))
            {
                var soundStream = new SoundStream(mp3StreamingPool.GetStream(id));
                Action<Stream, HttpContent, TransportContext> writeToStream = soundStream.WriteToStream;
                response.Content = new PushStreamContent(writeToStream, new MediaTypeHeaderValue("audio/mpeg"));
            }

            return response;
        }
    }
}
