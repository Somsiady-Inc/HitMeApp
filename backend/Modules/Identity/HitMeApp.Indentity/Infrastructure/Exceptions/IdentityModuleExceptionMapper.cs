using System;
using System.Net;
using HitMeApp.Indentity.Application.Exceptions;
using HitMeApp.Shared.DDD;
using HitMeApp.Shared.Infrastructure.Exceptions;

namespace HitMeApp.Indentity.Infrastructure.Exceptions
{
    internal sealed class IdentityModuleExceptionMapper : IExceptionToResponseMapper
    {
        private static readonly string _assemblyName = typeof(IdentityModuleExceptionMapper).Assembly.GetName().Name;

        public ExceptionResponse Map(Exception exception)
            => exception switch
            {
                UserAlreadyExistsException ex => new ExceptionResponse(ex.Message, HttpStatusCode.Conflict),
                DomainException ex => new ExceptionResponse(ex.Message),
                AppException ex => new ExceptionResponse(ex.Message),
                Exception => new ExceptionResponse($"Unknown error occured in the {_assemblyName} module", HttpStatusCode.InternalServerError),
                _ => null
            };
    }
}
