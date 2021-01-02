using System.Runtime.CompilerServices;
using Autofac;
using HitMeApp.Indentity.Application.Repositories;
using HitMeApp.Indentity.Contract.Clients;
using HitMeApp.Indentity.Core;
using HitMeApp.Indentity.Infrastructure.Dapper;
using HitMeApp.Indentity.Infrastructure.Exceptions;
using HitMeApp.Indentity.Infrastructure.IoC;
using HitMeApp.Shared.Infrastructure.Cqrs;
using HitMeApp.Shared.Infrastructure.Exceptions;
using HitMeApp.Shared.Infrastructure.Integration;
using HitMeApp.Shared.Infrastructure.Logging;
using HitMeApp.Shared.Infrastructure.Persistence.Postgres;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

// Some of the dynamic bindings require the internals to be visible
// TODO: Think about a better alternative
[assembly: InternalsVisibleTo("HitMeApp.Shared.Infrastructure")]

namespace HitMeApp.Indentity
{
    public static class IndentityModuleExtensions
    {
        public static IServiceCollection AddIdentityModule(this IServiceCollection services)
        {
            services.AddTransient<IIdentityModuleClient, DefaultIdentityModuleClient>();
            return services;
        }

        public static IApplicationBuilder UseIdentityModule(this IApplicationBuilder app)
        {
            var configuration = app.ApplicationServices.GetService<IConfiguration>();
            var containerBuilder = new ContainerBuilder();
            var logger = Log.Logger.ForModule("Identity");
            containerBuilder.RegisterInstance(logger).As<ILogger>().SingleInstance();
            containerBuilder.RegisterType<PasswordHasher<User>>().As<IPasswordHasher<User>>().InstancePerLifetimeScope();
            containerBuilder.AddCqrs();
            containerBuilder.UseInMemoryIntegrationEvents();
            containerBuilder.RegisterModule(new PostgresPersistenceIocModule(configuration));

            IdentityModuleCompositionRoot.SetContainer(containerBuilder.Build());

            app.RegisterExceptionMapperForThisModule<IdentityModuleExceptionMapper>();

            logger.Information("Identity module has been started successfully");
            return app;
        }
    }
}
