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

        public static implicit operator UserId(Guid id)
            => new UserId(id);
    }

    internal class User : Entity<UserId>
    {
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private init; }
        public DateTime UpdatedAt { get; private set; }

        public static User New(string email, string password, IUserPasswordService userPasswordService)
        {
            if (!IsEmailValid(email))
            {
                throw new InvalidEmailException(email);
            }

            var now = DateTime.UtcNow;
            return new User(Guid.NewGuid(), email, userPasswordService.GeneratePasswordForUser(password), now);
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

        public static User Load(Guid id, string email, string password, DateTime createdAt, DateTime updatedAt)
            => new User(id, email, password, createdAt, updatedAt);

        protected User()
        {
            // Frameworks usage
        }

        protected User(Guid id, string email, string password, DateTime createdAt, DateTime? updatedAt = null) : base(new UserId(id))
        {
            Email = email;
            Password = password;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt ?? createdAt;
        }
    }
}
