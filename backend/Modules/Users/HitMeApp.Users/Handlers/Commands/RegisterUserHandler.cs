using System.Net.Mail;
using System.Threading.Tasks;
using HitMeApp.Shared.Infrastructure.Cqrs.Commands;
using HitMeApp.Users.Contract.Commands;
using HitMeApp.Users.Exceptions;
using HitMeApp.Users.Infrastructure;
using HitMeApp.Users.Models;
using Microsoft.AspNetCore.Identity;

namespace HitMeApp.Users.Handlers.Commands
{
    internal sealed class RegisterUserHandler : ICommandHandler<RegisterUser>
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegisterUserHandler(IUserRepository repository, IPasswordHasher<User> passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        // TODO: JWT generator

        public async Task Handle(RegisterUser command)
        {
            if (!IsEmailValid(command.Email))
                throw new InvalidEmailException(command.Email);
            var hasEmailAlreadyBeenUsed = await _repository.Exists(command.Email);
            if (hasEmailAlreadyBeenUsed)
                throw new UserAlreadyExistsException(command.Email);
            if (!IsPasswordValid(command.Password))
                throw new InvalidPasswordException();

            var user = new User() { Email = command.Email };
            var hashedPassword = _passwordHasher.HashPassword(user, command.Password);
            user.Password = hashedPassword;
            // TODO: send integration event
            await _repository.Add(user);
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
