using System.Reflection;
using Autofac;
using HitMeApp.Shared.Infrastructure.Integration.EventProcessor;
using HitMeApp.Shared.Infrastructure.Integration.Memory;

namespace HitMeApp.Shared.Infrastructure.Integration
{
    public static class Extensions
    {
        public static void UseInMemoryIntegrationEvents(this ContainerBuilder builder, bool registerHandlersAutomatically = false)
        {
            builder.RegisterType<InMemoryIntegrationEventBusClient>()
                .As<IIntegrationEventBusClient>()
                .InstancePerLifetimeScope();

            builder.RegisterType<InMemoryIntegrationEventBusSubscriber>()
                .As<IIntegrationEventBusSubscriber>()
                .InstancePerLifetimeScope();

            if (registerHandlersAutomatically)
            {
                builder.RegisterAssemblyTypes(Assembly.GetCallingAssembly())
                    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>))
                    .InstancePerLifetimeScope();
            }
        }

        public static void UseEventProcessor<TEventMapper>(this ContainerBuilder builder)
            where TEventMapper : class, IEventMapper
        {
            builder.RegisterType<TEventMapper>()
                .As<IEventMapper>()
                .SingleInstance();

            builder.RegisterType<DefaultEventProcessor>()
                .As<IEventProcessor>()
                .InstancePerLifetimeScope();
        }
    }
}
