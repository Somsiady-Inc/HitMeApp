using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HitMeApp.Indentity.Core;

namespace HitMeApp.Indentity.Application.Repositories
{
    internal sealed class InMemoryUserRepository : IUserRepository
    {
        private static readonly ISet<User> _users = new HashSet<User>();

        public Task Add(User user)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }

        public Task Get(UserId id)
            => Task.FromResult(_users.SingleOrDefault(user => user.Id == id));
    }
}
