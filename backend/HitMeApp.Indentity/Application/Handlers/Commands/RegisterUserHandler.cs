using System;
using System.Threading.Tasks;
using HitMeApp.Indentity.Application.Exceptions;
using HitMeApp.Indentity.Application.Security;
using HitMeApp.Indentity.Contract.Commands;
using HitMeApp.Indentity.Core;
using HitMeApp.Indentity.Core.Policies;
using HitMeApp.Shared.Infrastructure.Cqrs.Commands;
using Microsoft.AspNetCore.Identity;

namespace HitMeApp.Indentity.Application.Handlers.Commands
{
    internal sealed class RegisterUserHandler : ICommandHandler<RegisterUser, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(RegisterUser command)
        {
            if (await _userRepository.Exists(command.Email))
            {
                throw new UserAlreadyExistsException(command.Email);
            }

            var passwordService = new PasswordHashedBasedUserPasswordService(_passwordHasher, new DefaultPasswordStrengthPolicy());
            var user = User.New(command.Email, command.Password, passwordService);
            await _userRepository.Add(user);
            return user.Id;
        }
    }
}
