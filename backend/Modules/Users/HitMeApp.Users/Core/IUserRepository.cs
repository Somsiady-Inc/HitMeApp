using System;
using System.Threading.Tasks;
using HitMeApp.Shared.DDD;

namespace HitMeApp.Users.Core
{
    internal interface IUserRepository : IRepository
    {
        public Task Add(User user);
        public Task<User> Get(UserId id);
        public Task Update(User user);
        public Task<bool> Exists(Guid id);
    }
}
