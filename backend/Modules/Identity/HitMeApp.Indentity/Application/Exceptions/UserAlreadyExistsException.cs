using HitMeApp.Shared.Infrastructure.Exceptions;

namespace HitMeApp.Indentity.Application.Exceptions
{
    internal class UserAlreadyExistsException : AppException
    {
        public override string Code => "user_already_exists";

        public string Email { get; }

        public UserAlreadyExistsException(string email) : base($"The user with this email already exists: {email}")
        {
            Email = email;
        }
    }
}
