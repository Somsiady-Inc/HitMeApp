using System.Threading.Tasks;

namespace HitMeApp.Indentity.Core
{
    internal interface IUserRepository
    {
        Task Add(User user);
        Task Get(UserId id);
    }
}
