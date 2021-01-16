using HitMeApp.Shared.DDD;

namespace HitMeApp.Indentity.Application.Exceptions
{
    internal class NewPasswordIsTheSameAsOldOneException : DomainException
    {
        public override string Code => "new_password_is_the_same_as_the_old_one";

        public NewPasswordIsTheSameAsOldOneException() : base($"New password is the same as the old one")
        {
        }
    }
}
