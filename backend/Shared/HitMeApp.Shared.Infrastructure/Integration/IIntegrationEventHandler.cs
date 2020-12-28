using System.Threading.Tasks;

namespace HitMeApp.Shared.Infrastructure.Integration
{
    public interface IIntegrationEventHandler<TIntegrationEvent> where TIntegrationEvent : IntegrationEvent
    {
        public Task Handle(TIntegrationEvent @event);
    }
}
