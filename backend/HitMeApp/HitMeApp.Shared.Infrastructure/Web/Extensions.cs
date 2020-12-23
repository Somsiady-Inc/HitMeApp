using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HitMeApp.Shared.Infrastructure.Web
{
    public static class Extensions
    {
        public static IServiceCollection RouteModuleControllers(this IServiceCollection services)
        {
            services.AddMvcCore().AddApplicationPart(Assembly.GetCallingAssembly()).AddControllersAsServices();
            return services;
        }
    }
}
