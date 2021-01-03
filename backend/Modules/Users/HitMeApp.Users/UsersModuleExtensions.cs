using System.Runtime.CompilerServices;
using Autofac;
using HitMeApp.Identity.IntegrationEvents;
using HitMeApp.Shared.Infrastructure.Cqrs;
using HitMeApp.Shared.Infrastructure.Exceptions;
using HitMeApp.Shared.Infrastructure.Integration;
using HitMeApp.Shared.Infrastructure.Logging;
using HitMeApp.Shared.Infrastructure.Web;
using HitMeApp.Users.Application.Handlers.IntegrationEvents;
using HitMeApp.Users.Contract.Clients;
using HitMeApp.Users.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

// Some of the dynamic bindings require the internals to be visible
// TODO: Think about a better alternative
[assembly: InternalsVisibleTo("HitMeApp.Shared.Infrastructure")]

namespace HitMeApp.Users
{
    public static class UsersModuleExtensions
    {
        public static IServiceCollection AddUsersModule(this IServiceCollection services)
        {
            services.RouteModuleControllers();
            services.AddTransient<IUserModuleClient, DefaultUserModuleClient>();
            return services;
        }

        public static IApplicationBuilder UseUsersModule(this IApplicationBuilder app)
        {
            var containerBuilder = new ContainerBuilder();
            var logger = Log.Logger.ForModule("Users");
            containerBuilder.RegisterInstance(logger).As<ILogger>().SingleInstance();
            containerBuilder.AddCqrs();
            containerBuilder.UseInMemoryIntegrationEvents(registerHandlersAutomatically: true);

            var container = containerBuilder.Build();
            UserModuleCompositionRoot.SetContainer(container);

            SubscribeToRelevantIntegrationEvents(container);

            app.RegisterExceptionMapperForThisModule<UsersModuleExceptionMapper>();

            logger.Information("User's module has been started successfully");

            return app;
        }

        private static void SubscribeToRelevantIntegrationEvents(IContainer container)
        {
            var busSubscriber = container.Resolve<IIntegrationEventBusSubscriber>();
            busSubscriber.Subscribe<UserRegistered, UserRegisteredHandler>();
        }
    }
}
