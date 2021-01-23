using System.Threading.Tasks;
using HitMeApp.Shared.Infrastructure.Cqrs.Commands;
using HitMeApp.Users.Application.Exceptions;
using HitMeApp.Users.Contract.Commands;
using HitMeApp.Users.Core;

namespace HitMeApp.Users.Application.Handlers.Commands
{
    internal sealed class ChangeLocationHandler : ICommandHandler<ChangeLocation>
    {
        private readonly IUserRepository _userRepository;

        public ChangeLocationHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(ChangeLocation command)
        {
            var user = await _userRepository.Get(command.UserId) ?? throw new UserNotFoundException(command.UserId);
            var newLocation = Location.New(command.Latitude, command.Longitude);
            user.ChangeLocation(newLocation);
            await _userRepository.Update(user);
        }
    }
}
