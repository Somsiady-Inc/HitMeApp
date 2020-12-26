using System.Net.Mail;
using System.Security;
using System.Threading.Tasks;
using HitMeApp.Shared.Infrastructure.Cqrs.Commands;
using HitMeApp.Users.Contract.Commands;
using HitMeApp.Users.Exceptions;
using HitMeApp.Users.Infrastructure;
using HitMeApp.Users.Models;

namespace HitMeApp.Users.Handlers.Commands
{
    internal sealed class RegisterUserHandler : ICommandHandler<RegisterUser>
    {
        private readonly IUserRepository _repository;

        public RegisterUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        // TODO: JWT generator

        public Task Handle(RegisterUser command)
        {
            if (!IsEmailValid(command.Email))
                throw new EmailNotValidException("Email is not valid.");
            if (_repository.Exists(command.Email))
                throw new UserAlreadyExistsException("User with that email already exists.");
            if (!IsPasswordValid(command.Password))
                throw new PasswordNotValidException("Password is not valid.");

            var user = new User() { Email = command.Email, Password = command.Password };
            // TODO: send integration event
            return _repository.Add(user);
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

        private static bool IsPasswordValid(string password)
        {
            return password != null && password.Length >= 8;
        }
    }
}
