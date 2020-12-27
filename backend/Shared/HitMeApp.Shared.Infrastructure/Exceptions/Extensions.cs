using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HitMeApp.Shared.Infrastructure.Exceptions
{
    public static class Extensions
    {
        public static IServiceCollection AddErrorHandler(this IServiceCollection services)
        {
            services.AddSingleton<IExceptionMapperRegistry, DefaultExceptionMapperRegistry>();
            return services;
        }

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
            return app;
        }

        public static IApplicationBuilder UseErrorHandler<TFallbackMapper>(this IApplicationBuilder app)
            where TFallbackMapper : class, IExceptionToResponseMapper, new()
        {
            app.UseErrorHandler();
            app.ApplicationServices.GetService<IExceptionMapperRegistry>().RegisterFallbackMapper<TFallbackMapper>();
            return app;
        }

        public static IApplicationBuilder RegisterErrorHandler<TExceptionMapper>(this IApplicationBuilder app, string namespaceRegex = null)
            where TExceptionMapper : class, IExceptionToResponseMapper, new()
        {
            app.ApplicationServices.GetService<IExceptionMapperRegistry>().Register<TExceptionMapper>(namespaceRegex);
            return app;
        }
    }
}
