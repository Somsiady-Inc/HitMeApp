using System.Threading.Tasks;

namespace HitMeApp.Shared.Infrastructure.Integration.Memory
{
    internal sealed class InMemoryIntegrationEventBusClient : IIntegrationEventBusClient
    {
        public Task Publish<TIntegrationEvent>(TIntegrationEvent @event) where TIntegrationEvent : IntegrationEvent
            => InMemoryIntegrationEventBus.Instance.Publish(@event);
    }
}
