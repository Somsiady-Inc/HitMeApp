using System.Collections.Generic;
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
    }
}
