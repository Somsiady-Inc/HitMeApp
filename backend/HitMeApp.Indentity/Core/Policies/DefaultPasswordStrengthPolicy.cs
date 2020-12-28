using HitMeApp.Indentity.Core.Exceptions;

namespace HitMeApp.Indentity.Core.Policies
{
    internal class DefaultPasswordStrengthPolicy : IPasswordStrengthPolicy
    {
        public void Test(string password)
        {
            if (!IsPasswordStrongEnough(password))
            {
                throw new PasswordTooWeakException(password);
            }
        }

        private static bool IsPasswordStrongEnough(string password)
            => !string.IsNullOrWhiteSpace(password) &&
            password.Length >= 8;
    }
}
