using Autofac;
using HitMeApp.Indentity.Application.Repositories;
using HitMeApp.Indentity.Core;
using HitMeApp.Indentity.Infrastructure.Dapper;
using HitMeApp.Shared.Infrastructure.Persistence.Postgres;
using Microsoft.Extensions.Configuration;

namespace HitMeApp.Indentity.Infrastructure.IoC
{
    public class PostgresPersistenceIocModule : Module
    {
        private readonly IConfiguration _configuration;

        public PostgresPersistenceIocModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PostgresUserRepository>()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();
            builder.UsePostgres(_configuration);
            IdentityModuleTypeHandlersPlugin.RegisterCustomTypeHandler();
        }
    }
}
