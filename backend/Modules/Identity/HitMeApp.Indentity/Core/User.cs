using System;
using System.Net.Mail;
using HitMeApp.Indentity.Core.Exceptions;
using HitMeApp.Indentity.Core.Services;
using HitMeApp.Shared.DDD;

namespace HitMeApp.Indentity.Core
{
    internal class UserId : EntityId
    {
        public UserId(Guid value) : base(value)
        {
        }
    }

    internal class User : Entity<UserId>
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public static User New(string email, string password, IUserPasswordService userPasswordService)
        {
            if (!IsEmailValid(email))
            {
                throw new InvalidEmailException(email);
            }

            return new User(Guid.NewGuid(), email, userPasswordService.GeneratePasswordForUser(password));
        }

        private static bool IsEmailValid(string email)
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

        public static User Load(Guid id, string email, string password)
            => new User(id, email, password);

        protected User(Guid id, string email, string password) : base(new UserId(id))
        {
            Email = email;
            Password = password;
        }
    }
}
