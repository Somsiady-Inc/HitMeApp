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
    internal sealed class LoginUserHandler : ICommandHandler<LoginUser, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public LoginUserHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(LoginUser command)
        {
            var user = await _userRepository.Get(command.Email)
                ?? throw new UserDoesNotExistException(command.Email);

            var userPasswordService = new PasswordHasherBasedUserPasswordService(_passwordHasher, new DefaultPasswordStrengthPolicy());
            if (!userPasswordService.Verify(user.Password, command.Password))
                throw new InvalidCredentialsException();

            await _userRepository.Save(user);
            return user.Id;
        }
    }
}
