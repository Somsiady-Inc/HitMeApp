using Autofac;
using HitMeApp.Shared.Infrastructure.Web;
using HitMeApp.Users.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HitMeApp.Users
{
    public static class UserModuleExtensions
    {
        public static IServiceCollection AddUserModule(this IServiceCollection services)
        {
            services.RouteModuleControllers();
            services.AddTransient<IUserModule, DefaultUserModule>();
            return services;
        }

        public static IApplicationBuilder UseUserModule(this IApplicationBuilder app)
        {
            var containerBuilder = new ContainerBuilder();
            UserModuleCompositionRoot.SetContainer(containerBuilder.Build());
            return app;
        }
    }
}
