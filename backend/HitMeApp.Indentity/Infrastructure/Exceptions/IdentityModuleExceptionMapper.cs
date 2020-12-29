using System;
using System.Net;
using HitMeApp.Indentity.Core.Exceptions;
using HitMeApp.Shared.Infrastructure.Exceptions;

namespace HitMeApp.Indentity.Infrastructure.Exceptions
{
    internal sealed class IdentityModuleExceptionMapper : IExceptionToResponseMapper
    {
        public ExceptionResponse Map(Exception exception)
            => exception switch
            {
                InvalidEmailException ex => new ExceptionResponse(ex.Message),
                PasswordTooWeakException ex => new ExceptionResponse(ex.Message),
                Exception => new ExceptionResponse("Unknown error occured in the users module", HttpStatusCode.InternalServerError),
                _ => null
            };
    }
}
