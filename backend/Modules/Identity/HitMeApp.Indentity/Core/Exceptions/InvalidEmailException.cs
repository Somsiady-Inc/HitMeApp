using HitMeApp.Shared.DDD;

namespace HitMeApp.Indentity.Core.Exceptions
{
    internal class InvalidEmailException : DomainException
    {
        public override string Code => "invalid_email";

        public string Email { get; }

        public InvalidEmailException(string email) : base($"Invalid email: {email}")
        {
            Email = email;
        }
    }
}
