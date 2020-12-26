using Autofac;
using HitMeApp.Shared.Infrastructure.Logging;
using HitMeApp.Shared.Infrastructure.Web;
using HitMeApp.Users.Contract;
using HitMeApp.Users.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

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
            containerBuilder.RegisterType<InMemoryUserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            UserModuleCompositionRoot.SetContainer(containerBuilder.Build());

            logger.Information("User's module has been started successfully");

            return app;
        }
    }
}
