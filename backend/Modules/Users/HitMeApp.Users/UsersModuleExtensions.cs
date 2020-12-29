using System.Runtime.CompilerServices;
using Autofac;
using HitMeApp.Shared.Infrastructure.Cqrs;
using HitMeApp.Shared.Infrastructure.Exceptions;
using HitMeApp.Shared.Infrastructure.Logging;
using HitMeApp.Shared.Infrastructure.Web;
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

            UserModuleCompositionRoot.SetContainer(containerBuilder.Build());

            app.RegisterExceptionMapperForThisModule<UsersModuleExceptionMapper>();

            logger.Information("User's module has been started successfully");

            return app;
        }
    }
}
