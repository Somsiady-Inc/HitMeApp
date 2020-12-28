using HitMeApp.Shared.Infrastructure.Exceptions;

namespace HitMeApp.Users.Exceptions
{
    internal class InvalidEmailException : AppException
    {
        public override string Code => "invalid_email";

        public string Email { get; }

        public InvalidEmailException(string email) : base($"Invalid email: {email}")
        {
            Email = email;
        }
    }
}
