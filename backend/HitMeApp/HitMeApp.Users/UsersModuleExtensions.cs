using Autofac;
using HitMeApp.Shared.Infrastructure.Web;
using HitMeApp.Users.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

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
            UserModuleCompositionRoot.SetContainer(containerBuilder.Build());
            return app;
        }
    }
}
