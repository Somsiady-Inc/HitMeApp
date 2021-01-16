using System.Threading.Tasks;

namespace HitMeApp.Indentity.Core
{
    internal interface IUserRepository
    {
        public Task Add(User user);
        public Task<User> Get(UserId id);
        public Task<User> Get(string email);
        public Task<bool> Exists(string email);
        public Task Save(User user);
    }
}
