using System.Collections.Generic;
using System.Threading.Tasks;

namespace HitMeApp.Shared.Infrastructure.Integration
{
    public interface IIntegrationEventBusClient
    {
        public Task Publish<TIntegrationEvent>(params TIntegrationEvent[] events) where TIntegrationEvent : IntegrationEvent;

        public Task Publish<TIntegrationEvent>(IEnumerable<TIntegrationEvent> events) where TIntegrationEvent : IntegrationEvent;
    }
}
