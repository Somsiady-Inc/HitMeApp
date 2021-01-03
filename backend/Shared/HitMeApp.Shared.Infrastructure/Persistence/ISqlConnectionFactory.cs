using System.Data;

namespace HitMeApp.Shared.Infrastructure.Persistence
{
    public interface ISqlConnectionFactory
    {
        public IDbConnection CreateNewOpenedConnection();
    }
}
