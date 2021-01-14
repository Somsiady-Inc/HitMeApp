using System.Threading.Tasks;
using HitMeApp.Shared.Infrastructure.Cqrs.Commands;
using HitMeApp.Users.Application.Exceptions;
using HitMeApp.Users.Contract.Commands;
using HitMeApp.Users.Contract.Dtos;
using HitMeApp.Users.Core;
using HitMeApp.Users.Core.Specification;

namespace HitMeApp.Users.Application.Handlers.Commands
{
    internal sealed class ChangePersonalInfoHandler : ICommandHandler<ChangePersonalInfo, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public ChangePersonalInfoHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(ChangePersonalInfo command)
        {
            var user = await _userRepository.Get(command.UserId) ?? throw new UserNotFoundException(command.UserId);
            var sex = Sex.From(command.Sex.GetValueOrDefault());
            var personalInfo = new PersonalInfo(command.Nickname, command.Description, command.BirthDate, sex, new MinimalAgeSpecification());
            user.ChangePersonalInfo(personalInfo);
            await _userRepository.Update(user);
            return user.AsDto();
        }
    }
}
