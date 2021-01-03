using System.Threading.Tasks;
using Dapper;
using HitMeApp.Indentity.Core;
using HitMeApp.Indentity.Infrastructure.Entities;
using HitMeApp.Shared.Infrastructure.Persistence;

namespace HitMeApp.Indentity.Infrastructure.Persistence.Repositories
{
    internal sealed class PostgresUserRepository : IUserRepository
    {
        private readonly ISqlQueryRunner _sqlExecutor;

        public PostgresUserRepository(ISqlQueryRunner sqlExecutor)
        {
            _sqlExecutor = sqlExecutor;
        }

        public Task Add(User user)
        {
            var query = $@"INSERT INTO identity.""user"" (id, email, password, created_at, updated_at)
                           VALUES (
                             @{nameof(User.Id)}, 
                             @{nameof(User.Email)}, 
                             @{nameof(User.Password)}, 
                             @{nameof(User.CreatedAt)}, 
                             @{nameof(User.UpdatedAt)}
                           )";

            var dbUser = user.AsDatabaseEntity();
            var parameters = new { dbUser.Id, dbUser.Email, dbUser.Password, dbUser.CreatedAt, dbUser.UpdatedAt };
            return _sqlExecutor.RunAsync(connection => connection.ExecuteAsync(query, parameters));
        }

        public Task<bool> Exists(string email)
        {
            var query = $@"SELECT COUNT(1) FROM identity.""user"" WHERE email=@{nameof(email)}";
            return _sqlExecutor.RunAsync(connection => connection.ExecuteScalarAsync<bool>(query, new { email }));
        }

        public async Task<User> Get(UserId id)
        {
            var query = $@"SELECT id, email, password, created_at, updated_at FROM identity.""user"" WHERE id=@{nameof(id)}";
            var dbUser = await _sqlExecutor.RunAsync(connection => connection.QueryFirstOrDefaultAsync<UserEntity>(query, new { id = id.Value }));
            return dbUser.AsDomainEntity();
        }
    }
}
