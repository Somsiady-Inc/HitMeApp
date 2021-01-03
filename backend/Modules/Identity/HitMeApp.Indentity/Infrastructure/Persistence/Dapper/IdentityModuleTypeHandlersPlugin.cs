using Dapper;

namespace HitMeApp.Indentity.Infrastructure.Persistence.Dapper
{
    internal static class IdentityModuleTypeHandlersPlugin
    {
        public static void RegisterCustomTypeHandlers()
        {
            SqlMapper.AddTypeHandler(new UserIdTypeHandler());
        }
    }
}
