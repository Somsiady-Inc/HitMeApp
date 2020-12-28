using Autofac;
using HitMeApp.Shared.Infrastructure.Integration.Memory;

namespace HitMeApp.Shared.Infrastructure.Integration
{
    public static class Extensions
    {
        public static void UseInMemoryIntegrationEvents(this ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryIntegrationEventBusClient>()
                .As<IIntegrationEventBusClient>()
                .InstancePerLifetimeScope();

            builder.RegisterType<InMemoryIntegrationEventBusSubscriber>()
                .As<IIntegrationEventBusSubscriber>()
                .InstancePerLifetimeScope();
        }
    }
}
