using System;
using System.Threading.Tasks;
using HitMeApp.Users.Core;

namespace HitMeApp.Users.Infrastructure.Persistence.Postgres.Repositories
{
    internal class PostgresUserRepository : IUserRepository
    {
        public Task Add(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Exists(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
