using System.Collections.Generic;
using System.Threading.Tasks;
using HitMeApp.Shared.DDD;

namespace HitMeApp.Users.Core
{
    internal interface IUserRepository : IRepository
    {
        public Task<IEnumerable<User>> Browse();
    }
}
