using HitMeApp.Shared.Infrastructure.Exceptions;

namespace HitMeApp.Users.Exceptions
{
    internal class InvalidPasswordException : AppException
    {
        public override string Code => "invalid_password";

        public InvalidPasswordException() : base("Password is not valid")
        {
        }
    }
}
