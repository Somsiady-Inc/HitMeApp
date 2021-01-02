using Dapper;

namespace HitMeApp.Indentity.Infrastructure.Dapper
{
    internal static class IdentityModuleTypeHandlersPlugin
    {
        public static void RegisterCustomTypeHandlers()
        {
            SqlMapper.AddTypeHandler(new UserIdTypeHandler());
        }
    }
}
