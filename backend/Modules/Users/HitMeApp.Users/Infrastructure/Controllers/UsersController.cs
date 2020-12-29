using HitMeApp.Shared.Infrastructure.Web;
using HitMeApp.Users.Contract.Clients;

namespace HitMeApp.Users.Infrastructure.Controllers
{
    public class UsersController : HitMeAppController
    {
        private readonly IUserModuleClient _userModuleClient;

        public UsersController(IUserModuleClient userModuleClient)
        {
            _userModuleClient = userModuleClient;
        }
    }
}
