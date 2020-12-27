using System;

namespace HitMeApp.Shared.Infrastructure.Exceptions
{
    public interface IExceptionToResponseMapper
    {
        ExceptionResponse Map(Exception ex);
    }
}
