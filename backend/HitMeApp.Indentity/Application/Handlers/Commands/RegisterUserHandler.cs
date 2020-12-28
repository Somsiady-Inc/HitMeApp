using System;
using System.Threading.Tasks;
using HitMeApp.Indentity.Contract.Commands;
using HitMeApp.Indentity.Core;
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

        public Task<Guid> Handle(RegisterUser command)
        {
            throw new System.NotImplementedException();
        }
    }
}
