using System.Threading.Tasks;
using HitMeApp.Shared.DDD;
using HitMeApp.Users.Models;

namespace HitMeApp.Users.Infrastructure
{
    internal interface IUserRepository : IRepository
    {
        public Task Add(User user);
        public Task<bool> Exists(string email);
    }
}
