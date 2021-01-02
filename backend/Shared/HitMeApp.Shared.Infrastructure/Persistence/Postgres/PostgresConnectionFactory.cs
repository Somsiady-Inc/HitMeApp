using System.Data;
using Npgsql;

namespace HitMeApp.Shared.Infrastructure.Persistence.Postgres
{
    internal sealed class PostgresConnectionFactory : ISqlConnectionFactory
    {
        private readonly DatabaseSettings _databaseSettings;

        public PostgresConnectionFactory(DatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public IDbConnection Connect()
        {
            var connectionString = $"Host={_databaseSettings.Host};" +
                $"Username={_databaseSettings.Username};" +
                $"Password={_databaseSettings.Password};" +
                $"Database={_databaseSettings.DatabaseName}";
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
