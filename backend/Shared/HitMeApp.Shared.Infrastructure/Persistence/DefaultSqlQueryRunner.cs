using System;
using System.Data;
using System.Threading.Tasks;

namespace HitMeApp.Shared.Infrastructure.Persistence
{
    internal sealed class DefaultSqlQueryRunner : ISqlQueryRunner
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public DefaultSqlQueryRunner(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void Run(Action<IDbConnection> action)
        {
            using var connection = _connectionFactory.CreateNewOpenedConnection();
            action(connection);
        }

        public TResult Run<TResult>(Func<IDbConnection, TResult> action)
        {
            using var connection = _connectionFactory.CreateNewOpenedConnection();
            return action(connection);
        }

        public async Task<TResult> RunAsync<TResult>(Func<IDbConnection, Task<TResult>> action)
        {
            using var connection = _connectionFactory.CreateNewOpenedConnection();
            return await action(connection);
        }
    }
}
