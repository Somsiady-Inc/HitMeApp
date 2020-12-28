using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HitMeApp.Users.Models;

namespace HitMeApp.Users.Infrastructure
{
    internal sealed class InMemoryUserRepository : IUserRepository
    {
        private static readonly ISet<User> _users = new HashSet<User>();

        public Task Add(User user)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<User>> Browse()
            => Task.FromResult(_users.AsEnumerable());

        public Task<bool> Exists(string email)
            => Task.FromResult(_users.Any(u => u.Email == email));
    }
}
