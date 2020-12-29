using HitMeApp.Shared.DDD;

namespace HitMeApp.Indentity.Core.Exceptions
{
    internal class PasswordTooWeakException : DomainException
    {
        public override string Code => "password_too_weak";

        public string RulesDescription { get; }

        public PasswordTooWeakException(string rulesDescription) : base($"A valid password requires: {rulesDescription}")
        {
            RulesDescription = rulesDescription;
        }
    }
}
