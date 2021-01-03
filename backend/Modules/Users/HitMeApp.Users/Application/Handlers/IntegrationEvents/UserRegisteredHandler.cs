using System.Threading.Tasks;
using HitMeApp.Identity.IntegrationEvents;
using HitMeApp.Shared.Infrastructure.Integration;
using Serilog;

namespace HitMeApp.Users.Application.Handlers.IntegrationEvents
{
    internal sealed class UserRegisteredHandler : IIntegrationEventHandler<UserRegistered>
    {
        private readonly ILogger _logger;

        public UserRegisteredHandler(ILogger logger)
        {
            _logger = logger;
        }

        public Task Handle(UserRegistered @event)
        {
            // TODO: Implementation needed. For now it's just a showcase of how integration events operate
            _logger.Information("Handling {eventName} event with payload: {@event}", nameof(UserRegistered), @event);
            return Task.CompletedTask;
        }
    }
}
