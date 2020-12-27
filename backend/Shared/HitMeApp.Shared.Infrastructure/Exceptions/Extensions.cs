using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HitMeApp.Shared.Infrastructure.Exceptions
{
    public static class Extensions
    {
        public static IServiceCollection AddErrorHandler(this IServiceCollection services)
        {
            services.AddTransient<ErrorHandlerMiddleware>();
            services.AddSingleton<IExceptionMapperRegistry, DefaultExceptionMapperRegistry>();
            return services;
        }

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
            return app;
        }

        public static IApplicationBuilder UseErrorHandler<TFallbackExceptionMapper>(this IApplicationBuilder app)
            where TFallbackExceptionMapper : class, IExceptionToResponseMapper, new()
        {
            app.UseErrorHandler();
            app.ApplicationServices.GetService<IExceptionMapperRegistry>().RegisterFallbackMapper<TFallbackExceptionMapper>();
            return app;
        }

        public static IApplicationBuilder RegisterExceptionMapper<TExceptionMapper>(this IApplicationBuilder app, string namespaceRegex = null)
            where TExceptionMapper : class, IExceptionToResponseMapper, new()
        {
            app.ApplicationServices.GetService<IExceptionMapperRegistry>().Register<TExceptionMapper>(namespaceRegex);
            return app;
        }

        internal static bool IsDefault<T>(this T obj)
            => default(T).Equals(obj);
    }
}
