using Autofac;
using HitMeApp.Shared.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace HitMeApp.Shared.Infrastructure.Security.Jwt
{
    public static class Extensions
    {
        public static ContainerBuilder UseJwt(this ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterInstance(configuration.GetSettings<JwtSettings>()).SingleInstance();
            builder.RegisterType<JwtHandler>().As<IJwtHandler>().InstancePerLifetimeScope();
            return builder;
        }
    }
}
