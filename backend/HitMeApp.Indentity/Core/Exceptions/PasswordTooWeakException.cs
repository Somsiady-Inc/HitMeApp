using HitMeApp.Shared.DDD;

namespace HitMeApp.Indentity.Core.Exceptions
{
    internal class PasswordTooWeakException : DomainException
    {
        public override string Code => "password_too_weak";

        public PasswordTooWeakException(string message) : base(message)
        {
        }
    }
}
