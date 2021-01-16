using System.Net.Mail;
using HitMeApp.Indentity.Core.Exceptions;

namespace HitMeApp.Indentity.Core.Policies
{
    internal class DefaultEmailValidityPolicy : IEmailValidityPolicy
    {
        public void Validate(string email)
        {
            if (!IsValid(email))
                throw new InvalidEmailException(email);

            // TODO: discard temporary mails, obviously-fake mails etc.
        }

        private static bool IsValid(string email)
        {
            try
            {
                var emailAddress = new MailAddress(email);
                return emailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
