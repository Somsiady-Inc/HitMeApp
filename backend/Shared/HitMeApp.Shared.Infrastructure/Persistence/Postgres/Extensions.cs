using Autofac;
using HitMeApp.Shared.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace HitMeApp.Shared.Infrastructure.Persistence.Postgres
{
    public static class Extensions
    {
        public static ContainerBuilder UsePostgres(this ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterType<PostgresConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterSettings<DatabaseSettings>(configuration);

            builder.RegisterType<DefaultSqlQueryRunner>()
                .As<ISqlQueryRunner>()
                .InstancePerLifetimeScope()
                .PreserveExistingDefaults();

            return builder;
        }
    }
}
