using System;
using System.Threading.Tasks;
using Dapper;
using HitMeApp.Shared.Infrastructure.Persistence;
using HitMeApp.Users.Core;
using HitMeApp.Users.Infrastructure.Persistence.Postgres.Entities;

namespace HitMeApp.Users.Infrastructure.Persistence.Postgres.Repositories
{
    internal class PostgresUserRepository : IUserRepository
    {
        private readonly ISqlQueryRunner _sqlQueryRunner;

        public PostgresUserRepository(ISqlQueryRunner sqlQueryRunner)
        {
            _sqlQueryRunner = sqlQueryRunner;
        }

        public Task Add(User user)
        {
            var query = $@"INSERT INTO ""user"".""user"" (id) VALUES (@{nameof(user.Id)})";
            var userEntity = user.AsDatabaseEntity();
            return _sqlQueryRunner.RunAsync(connection => connection.ExecuteAsync(query, new { userEntity.Id }));
        }

        public Task Update(User user)
        {
            var updateUserQuery = $@"
                UPDATE ""user"".""user""
                SET 
                    nickname    = @{nameof(PersonalInfo.Nickname)}, 
                    description = @{nameof(PersonalInfo.Description)},
                    birth_date  = @{nameof(PersonalInfo.BirthDate)},
                    sex         = @{nameof(PersonalInfo.Sex)},
                    latitude    = @{nameof(Location.Latitude)},
                    longitude   = @{nameof(Location.Longitude)}
                WHERE id = @{nameof(User.Id)}
            ";

            var userEntity = user.AsDatabaseEntity();

            return _sqlQueryRunner.RunAsync(async connection =>
            {
                await connection.ExecuteAsync(updateUserQuery, new
                {
                    userEntity.Id,
                    userEntity.Nickname,
                    userEntity.Description,
                    userEntity.BirthDate,
                    userEntity.Sex,
                    userEntity.Latitude,
                    userEntity.Longitude,
                });

                return Task.CompletedTask;
            });
        }

        public Task<bool> Exists(Guid id)
        {
            var query = $@"SELECT COUNT(1) FROM ""user"".""user"" WHERE id=@{nameof(id)}";
            return _sqlQueryRunner.RunAsync(connection => connection.ExecuteScalarAsync<bool>(query, new { id }));
        }
    }
}
