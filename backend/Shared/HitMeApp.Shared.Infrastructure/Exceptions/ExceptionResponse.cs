using System.Net;

namespace HitMeApp.Shared.Infrastructure.Exceptions
{
    public class ExceptionResponse
    {
        public object Response { get; }
        public HttpStatusCode StatusCode { get; }

        public ExceptionResponse(object response, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            Response = response;
            StatusCode = statusCode;
        }
    }
}
