using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp.Http
{
    class HttpException: Exception
    {
        public string Url { get; }
        public HttpResponseMessage Response { get;  }
        public HttpStatusCode Status { get { return Response.StatusCode; } }
        public string Reason { get; }

        public HttpException(string url, HttpResponseMessage response, string reason)
        {
            Url = url;
            Response = response;
            Reason = reason;
        }
    }
}
