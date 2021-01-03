using HitMeApp.Indentity.Core.Exceptions;

namespace HitMeApp.Indentity.Core.Policies
{
    internal class DefaultPasswordStrengthPolicy : IPasswordStrengthPolicy
    {
        public void Validate(string password)
        {
            if (!IsPasswordStrongEnough(password))
            {
                throw new PasswordTooWeakException("at least 8 characters");
            }
        }

        // TODO: Agree on password stregth rules
        private static bool IsPasswordStrongEnough(string password)
            => !string.IsNullOrWhiteSpace(password) && password.Length >= 8;
    }
}
