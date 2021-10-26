using System;
using System.Net;

namespace Intive.ConfR.Application.Exceptions
{
    public class GraphApiException : Exception
    {
        public int StatusCode { get; set; }

        public GraphApiException(HttpStatusCode code, string message)
            : base($"Graph error {(int)code}: {message}")
        {
            StatusCode = (int)code;
        }

    }
}
