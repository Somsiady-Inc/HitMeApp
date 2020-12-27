using System;
using System.Net;
using HitMeApp.Shared.Infrastructure.Exceptions;

namespace HitMeApp.Api.Exceptions
{
    public class GlobalFallbackExceptionMapper : IExceptionToResponseMapper
    {
        public ExceptionResponse Map(Exception exception)
            => exception switch
            {
                InvalidOperationException ex => new ExceptionResponse(ex.Message, HttpStatusCode.BadRequest),
                _ => new ExceptionResponse("Unknown error", HttpStatusCode.InternalServerError)
            };
    }
}
