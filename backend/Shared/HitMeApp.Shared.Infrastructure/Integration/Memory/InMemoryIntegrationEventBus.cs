using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HitMeApp.Shared.Infrastructure.Integration.Subscriptions;

namespace HitMeApp.Shared.Infrastructure.Integration.Memory
{
    internal class InMemoryIntegrationEventBus
    {
        private static readonly Lazy<InMemoryIntegrationEventBus> _instance = new Lazy<InMemoryIntegrationEventBus>(() => new InMemoryIntegrationEventBus());

        private readonly List<IIntegrationEventSubscription> _subscriptions = new List<IIntegrationEventSubscription>();

        public static InMemoryIntegrationEventBus Instance => _instance.Value;

        private InMemoryIntegrationEventBus()
        {
        }

        public Task Publish<TIntegrationEvent>(TIntegrationEvent @event) where TIntegrationEvent : IntegrationEvent
        {
            var handlers = _subscriptions.Where(subscription => subscription as IntegrationEventSubscription<TIntegrationEvent> is { })
                .Cast<IntegrationEventSubscription<TIntegrationEvent>>()
                .Select(subscription => subscription.HandlerResolver().Handle(@event))
                .ToList();

            return Task.WhenAll(handlers);
        }

        public void Subscribe<TIntegrationEvent>(Func<IIntegrationEventHandler<TIntegrationEvent>> handlerResolver)
            where TIntegrationEvent : IntegrationEvent
        {
            _subscriptions.Add(new IntegrationEventSubscription<TIntegrationEvent>(handlerResolver));
        }
    }
}
