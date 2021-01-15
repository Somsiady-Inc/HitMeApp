using System;
using System.Threading.Tasks;
using HitMeApp.Indentity.Application.Exceptions;
using HitMeApp.Indentity.Application.Security;
using HitMeApp.Indentity.Contract.Commands;
using HitMeApp.Indentity.Core;
using HitMeApp.Indentity.Core.Policies;
using HitMeApp.Shared.Infrastructure.Cqrs.Commands;
using HitMeApp.Shared.Infrastructure.Security.Jwt;
using Microsoft.AspNetCore.Identity;

namespace HitMeApp.Indentity.Application.Handlers.Commands
{
    internal sealed class LoginUserHandler : ICommandHandler<LoginUser, JsonWebToken>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtHandler _jwtHandler;

        public LoginUserHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtHandler = jwtHandler;
        }
        public async Task<JsonWebToken> Handle(LoginUser command)
        {
            var user = await _userRepository.Get(command.Email)
                ?? throw new UserDoesNotExistException(command.Email);

            var userPasswordService = new PasswordHasherBasedUserPasswordService(_passwordHasher, new DefaultPasswordStrengthPolicy());
            if (!userPasswordService.Verify(user.Password, command.Password))
                throw new InvalidCredentialsException();

            return _jwtHandler.Create(user.Email, user.Id);
        }
    }
}
