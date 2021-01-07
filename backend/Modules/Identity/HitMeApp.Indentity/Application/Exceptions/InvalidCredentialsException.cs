using HitMeApp.Shared.Infrastructure.Exceptions;

namespace HitMeApp.Indentity.Application.Exceptions
{
    class InvalidCredentialsException : AppException
    {
        public override string Code => "invalid_credentials";

        public InvalidCredentialsException() : base($"Invalid credentials")
        {
        }
    }
}
