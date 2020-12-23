using Autofac;
using HitMeApp.Shared.Infrastructure.Cqrs.Commands;
using HitMeApp.Shared.Infrastructure.Cqrs.Events;
using HitMeApp.Shared.Infrastructure.Cqrs.Queries;
using System.Reflection;

namespace HitMeApp.Shared.Infrastructure.Cqrs
{
    public static class Extensions
    {
        public static ContainerBuilder AddCqrs(this ContainerBuilder containerBuilder, Assembly assembly = null)
        {
            return containerBuilder
                .AddCqrsCommands(assembly)
                .AddCqrsQueries(assembly)
                .AddCqrsEvents(assembly);
        }
        public static ContainerBuilder AddCqrsCommands(this ContainerBuilder containerBuilder, Assembly assembly = null)
        {
            var finalAssembly = assembly ?? Assembly.GetCallingAssembly();

            containerBuilder.RegisterAssemblyTypes(new[] { finalAssembly })
                .Where(t => t.IsAssignableFrom(typeof(ICommandHandler<>)) || t.IsAssignableFrom(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterType<InMemoryCommandBus>()
                .As<ICommandBus>()
                .InstancePerLifetimeScope();

            return containerBuilder;
        }

        public static ContainerBuilder AddCqrsQueries(this ContainerBuilder containerBuilder, Assembly assembly = null)
        {
            var finalAssembly = assembly ?? Assembly.GetCallingAssembly();

            containerBuilder.RegisterAssemblyTypes(new[] { finalAssembly })
                .Where(t => t.IsAssignableFrom(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterType<InMemoryQueryBus>()
                .As<IQueryBus>()
                .InstancePerLifetimeScope();

            return containerBuilder;
        }

        public static ContainerBuilder AddCqrsEvents(this ContainerBuilder containerBuilder, Assembly assembly = null)
        {
            var finalAssembly = assembly ?? Assembly.GetCallingAssembly();

            containerBuilder.RegisterAssemblyTypes(new[] { finalAssembly })
                .Where(t => t.IsAssignableFrom(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterType<InMemoryEventBus>()
                .As<IEventBus>()
                .InstancePerLifetimeScope();

            return containerBuilder;
        }
    }
}
