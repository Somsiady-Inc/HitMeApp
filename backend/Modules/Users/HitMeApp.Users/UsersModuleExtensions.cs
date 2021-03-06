﻿using System.Runtime.CompilerServices;
using Autofac;
using HitMeApp.Identity.IntegrationEvents;
using HitMeApp.Shared.Infrastructure.Cqrs;
using HitMeApp.Shared.Infrastructure.Exceptions;
using HitMeApp.Shared.Infrastructure.Integration;
using HitMeApp.Shared.Infrastructure.Logging;
using HitMeApp.Shared.Infrastructure.Persistence.Postgres;
using HitMeApp.Shared.Infrastructure.Web;
using HitMeApp.Users.Application.Handlers.IntegrationEvents;
using HitMeApp.Users.Contract.Clients;
using HitMeApp.Users.Core;
using HitMeApp.Users.Infrastructure.Exceptions;
using HitMeApp.Users.Infrastructure.Persistence.Postgres.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

[assembly: InternalsVisibleTo("HitMeApp.Shared.Infrastructure")]
[assembly: InternalsVisibleTo("HitMeApp.Users.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

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
            var configuration = app.ApplicationServices.GetService<IConfiguration>();
            var containerBuilder = new ContainerBuilder();
            var logger = Log.Logger.ForModule("Users");
            containerBuilder.RegisterInstance(logger).As<ILogger>().SingleInstance();
            containerBuilder.AddCqrs();
            containerBuilder.RegisterType<PostgresUserRepository>()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();
            containerBuilder.UsePostgres(configuration);
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

        internal static bool EqualsIgnoreCase(this string a, string b)
            => b is { } && a.ToLowerInvariant() == b.ToLowerInvariant();
    }
}
