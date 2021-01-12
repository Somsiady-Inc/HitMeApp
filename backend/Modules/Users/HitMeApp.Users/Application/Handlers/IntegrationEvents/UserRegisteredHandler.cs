using System.Threading.Tasks;
using HitMeApp.Identity.IntegrationEvents;
using HitMeApp.Shared.Infrastructure.Integration;
using HitMeApp.Users.Application.Exceptions;
using HitMeApp.Users.Core;

namespace HitMeApp.Users.Application.Handlers.IntegrationEvents
{
    internal sealed class UserRegisteredHandler : IIntegrationEventHandler<UserRegistered>
    {
        private readonly IUserRepository _userRepository;

        public UserRegisteredHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UserRegistered @event)
        {
            if (await _userRepository.Exists(@event.UserId))
            {
                throw new UserAlreadyExistsException(@event.UserId);
            }

            var user = User.Incomplete(@event.UserId);
            await _userRepository.Add(user);
        }
    }
}
