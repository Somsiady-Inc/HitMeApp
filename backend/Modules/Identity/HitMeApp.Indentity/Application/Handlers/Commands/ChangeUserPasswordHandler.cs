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
    internal sealed class ChangeUserPasswordHandler : ICommandHandler<ChangeUserPassword>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public ChangeUserPasswordHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(ChangeUserPassword command)
        {
            var user = await _userRepository.Get(command.Guid)
                ?? throw new UserDoesNotExistException(command.Guid);

            var userPasswordService = new PasswordHasherBasedUserPasswordService(_passwordHasher, new DefaultPasswordStrengthPolicy());
            if (!userPasswordService.Verify(user.Password, command.CurrentPassword))
                throw new InvalidCredentialsException();

            user.ChangePassword(command.NewPassword, userPasswordService);
            await _userRepository.Save(user);
        }
    }
}
