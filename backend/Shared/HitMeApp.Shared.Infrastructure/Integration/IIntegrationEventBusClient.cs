using System.Threading.Tasks;

namespace HitMeApp.Shared.Infrastructure.Integration
{
    public interface IIntegrationEventBusClient
    {
        public Task Publish<TIntegrationEvent>(TIntegrationEvent @event) where TIntegrationEvent : IntegrationEvent;
    }
}
