using Newtonsoft.Json;
using System;
using System.Net;

namespace Catalog.API.Extensions
{
    public class ExceptionBusinessLog : Exception
    {
        public HttpStatusCode StatusCode;

        [JsonProperty("info_type")]
        public string InfoType = "BusinessInfo";

        public ExceptionBusinessLog() { }

        public ExceptionBusinessLog(string message, Exception innerException)
            : base(message, innerException) { }

        public ExceptionBusinessLog(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
