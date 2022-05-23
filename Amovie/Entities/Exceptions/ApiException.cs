using System.Net;

namespace Entities.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public ApiException(HttpStatusCode code, string message) : base(message)
        {
            StatusCode = code;
        }
    }
}
