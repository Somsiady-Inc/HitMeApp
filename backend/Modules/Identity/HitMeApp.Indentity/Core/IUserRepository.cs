using System.Threading.Tasks;

namespace HitMeApp.Indentity.Core
{
    internal interface IUserRepository
    {
        Task Add(User user);
        Task<User> Get(UserId id);
        public Task<bool> Exists(string email);
        Task Save(User user);
    }
}
