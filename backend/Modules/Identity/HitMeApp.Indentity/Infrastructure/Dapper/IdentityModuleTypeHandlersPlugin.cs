using Dapper;

namespace HitMeApp.Indentity.Infrastructure.Dapper
{
    internal static class IdentityModuleTypeHandlersPlugin
    {
        public static void RegisterCustomTypeHandler()
        {
            SqlMapper.AddTypeHandler(new UserIdTypeHandler());
        }
    }
}
