using System;
using System.Net;
using HitMeApp.Shared.Infrastructure.Exceptions;

namespace HitMeApp.Indentity.Infrastructure.Exceptions
{
    internal sealed class IdentityModuleExceptionMapper : IExceptionToResponseMapper
    {
        public ExceptionResponse Map(Exception exception)
            => exception switch
            {
                Exception => new ExceptionResponse("Unknown error occured in the users module", HttpStatusCode.BadRequest),
                _ => null
            };
    }
}
